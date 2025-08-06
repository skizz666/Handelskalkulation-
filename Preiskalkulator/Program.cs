using System;
using System.Collections.Generic;
using System.Linq;

namespace Preiskalkulator
{
    // Represents a single calculation step, like:
    // (input1, input2, ...) -> formula -> output
    public class Calculation
    {
        public string Output { get; }
        public string[] Inputs { get; }
        public Func<double[], double> Formula { get; }

        public Calculation(string output, string[] inputs, Func<double[], double> formula)
        {
            Output = output;
            Inputs = inputs;
            Formula = formula;
        }
    }

    // The calculation engine
    public class Calculator
    {
        private readonly List<Calculation> _calculations = new List<Calculation>();
        private readonly Dictionary<string, double> _values;
        private readonly HashSet<string> _currentlyCalculating = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public Calculator(Dictionary<string, double> initialValues)
        {
            // Use a case-insensitive dictionary for variable names
            _values = new Dictionary<string, double>(initialValues, StringComparer.OrdinalIgnoreCase);
            RegisterCalculations();
        }

        // Gets a value, calculating it if it's not already known.
        public double GetValue(string name)
        {
            // If we already have the value, return it.
            if (_values.TryGetValue(name, out var value))
            {
                return value;
            }

            // Check for circular dependencies.
            if (_currentlyCalculating.Contains(name))
            {
                throw new InvalidOperationException($"Circular dependency detected for '{name}'.");
            }

            // Find all registered calculations that can produce the desired value.
            var possibleCalculations = _calculations.Where(c => c.Output.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!possibleCalculations.Any())
            {
                throw new InvalidOperationException($"Cannot calculate '{name}'. No formula is available.");
            }

            _currentlyCalculating.Add(name);

            try
            {
                // Try each possible calculation until one succeeds.
                foreach (var calculation in possibleCalculations)
                {
                    try
                    {
                        // Recursively get all input values for the current formula.
                        var inputValues = new double[calculation.Inputs.Length];
                        for (int i = 0; i < calculation.Inputs.Length; i++)
                        {
                            inputValues[i] = GetValue(calculation.Inputs[i]);
                        }

                        // Execute the formula and cache the result.
                        var result = calculation.Formula(inputValues);
                        _values[name] = result;
                        Console.WriteLine($"... Calculated {name}: {result:F2}");
                        return result;
                    }
                    catch (InvalidOperationException)
                    {
                        // This calculation path failed, try the next one.
                        continue;
                    }
                }
            }
            finally
            {
                // Make sure to remove the variable from the set, so it can be used in other calculation paths.
                _currentlyCalculating.Remove(name);
            }


            // If we've tried all possible calculations and none succeeded, throw.
            throw new InvalidOperationException($"Could not calculate '{name}' with the available inputs.");
        }

        // Registers all the formulas from the Preiskalkulator class.
        private void RegisterCalculations()
        {
            // --- Vorwärtskalkulationen ---
            _calculations.Add(new Calculation("nettoeinkaufspreis", new[] { "bruttoeinkaufspreis", "mwstsatz" }, args => Preiskalkulator.BerechneNettoEinkaufspreis(args[0], args[1])));
            _calculations.Add(new Calculation("zieleinkaufspreis", new[] { "nettoeinkaufspreis", "lieferantenrabatt" }, args => Preiskalkulator.BerechneZieleinkaufspreis(args[0], args[1])));
            _calculations.Add(new Calculation("bareinkaufspreis", new[] { "zieleinkaufspreis", "lieferantenskonto" }, args => Preiskalkulator.BerechneBareinkaufspreis(args[0], args[1])));
            _calculations.Add(new Calculation("bezugspreis", new[] { "bareinkaufspreis", "bezugskosten" }, args => Preiskalkulator.BerechneBezugspreis(args[0], args[1])));
            _calculations.Add(new Calculation("selbstkostenpreis", new[] { "bezugspreis", "gemeinkostenzuschlag" }, args => Preiskalkulator.BerechneSelbstkostenpreis(args[0], args[1])));
            _calculations.Add(new Calculation("barverkaufspreis", new[] { "selbstkostenpreis", "gewinnaufschlag" }, args => Preiskalkulator.BerechneBarverkaufspreis(args[0], args[1])));
            _calculations.Add(new Calculation("zielverkaufspreis", new[] { "barverkaufspreis", "kundenskonto", "verkaufsprovision" }, args => Preiskalkulator.BerechneZielverkaufspreis(args[0], args[1], args[2])));
            _calculations.Add(new Calculation("nettoverkaufspreis", new[] { "zielverkaufspreis", "kundenrabatt" }, args => Preiskalkulator.BerechneNettoverkaufspreis(args[0], args[1])));
            _calculations.Add(new Calculation("bruttoverkaufspreis", new[] { "nettoverkaufspreis", "mwstsatz" }, args => Preiskalkulator.BerechneBruttoverkaufspreis(args[0], args[1])));

            // --- Rückwärtskalkulationen ---
            _calculations.Add(new Calculation("nettoverkaufspreis", new[] { "bruttoverkaufspreis", "mwstsatz" }, args => Preiskalkulator.RueckBerechneNettoverkaufspreis(args[0], args[1])));
            _calculations.Add(new Calculation("zielverkaufspreis", new[] { "nettoverkaufspreis", "kundenrabatt" }, args => Preiskalkulator.RueckBerechneZielverkaufspreis(args[0], args[1])));
            _calculations.Add(new Calculation("barverkaufspreis", new[] { "zielverkaufspreis", "kundenskonto", "verkaufsprovision" }, args => Preiskalkulator.RueckBerechneBarverkaufspreis(args[0], args[1], args[2])));
            _calculations.Add(new Calculation("selbstkostenpreis", new[] { "barverkaufspreis", "gewinnaufschlag" }, args => Preiskalkulator.RueckBerechneSelbstkostenpreis(args[0], args[1])));
            _calculations.Add(new Calculation("bezugspreis", new[] { "selbstkostenpreis", "gemeinkostenzuschlag" }, args => Preiskalkulator.RueckBerechnungBezugspreis(args[0], args[1])));
            _calculations.Add(new Calculation("bareinkaufspreis", new[] { "bezugspreis", "bezugskosten" }, args => Preiskalkulator.RueckBerechneBareinkaufspreis(args[0], args[1])));
            _calculations.Add(new Calculation("zieleinkaufspreis", new[] { "bareinkaufspreis", "lieferantenskonto" }, args => Preiskalkulator.RueckBerechneZieleinkaufspreis(args[0], args[1])));
            _calculations.Add(new Calculation("nettoeinkaufspreis", new[] { "zieleinkaufspreis", "lieferantenrabatt" }, args => Preiskalkulator.RueckBerechneNettoeinkaufspreis(args[0], args[1])));
            _calculations.Add(new Calculation("bruttoeinkaufspreis", new[] { "nettoeinkaufspreis", "mwstsatz" }, args => Preiskalkulator.RueckBerechnungBruttoeinkaufspreis(args[0], args[1])));
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var values = new Dictionary<string, double>();
            string gesucht = "";

            // The order of variables here determines the order they are asked in the UI.
            var variableNames = new[]
            {
                "bruttoeinkaufspreis",
                "lieferantenrabatt",
                "lieferantenskonto",
                "bezugskosten",
                "gemeinkostenzuschlag",
                "gewinnaufschlag",
                "kundenskonto",
                "verkaufsprovision",
                "kundenrabatt",
                "mwstsatz",
                "bruttoverkaufspreis"
            };

            Console.WriteLine("--- Interaktiver Preiskalkulator ---");
            Console.WriteLine("Bitte geben Sie die bekannten Werte ein. Lassen Sie das Feld für den gesuchten Wert leer.");

            foreach (var name in variableNames)
            {
                Console.Write($"{name}: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    if (!string.IsNullOrEmpty(gesucht))
                    {
                        Console.WriteLine("Fehler: Es kann nur ein Wert auf einmal berechnet werden.");
                        return;
                    }
                    gesucht = name;
                }
                else if (double.TryParse(input, out double value))
                {
                    values[name] = value;
                }
                else
                {
                    Console.WriteLine($"Fehler: Ungültige Eingabe für {name}. Bitte geben Sie eine Zahl ein.");
                    return;
                }
            }

            if (string.IsNullOrEmpty(gesucht))
            {
                Console.WriteLine("Fehler: Es wurde kein zu suchender Wert angegeben (alle Felder ausgefüllt).");
                return;
            }

            Console.WriteLine($"\nGesuchter Wert: {gesucht}\n");
            Console.WriteLine("Gegebene Werte:");
            foreach (var kvp in values)
            {
                Console.WriteLine($"- {kvp.Key}: {kvp.Value}");
            }
            Console.WriteLine("\nRechenweg:");

            // --- CALCULATION ---
            var calculator = new Calculator(values);
            try
            {
                double result = calculator.GetValue(gesucht);
                Console.WriteLine($"\n--- ERGEBNIS ---\n{gesucht}: {result:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n--- FEHLER ---\nKonnte '{gesucht}' nicht berechnen: {ex.Message}");
            }
        }
    }
}
