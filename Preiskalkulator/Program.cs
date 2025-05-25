
double mwstsatz = 19.0;
double bruttoeinkaufspreis = 3100.0;
double lieferantenrabatt = 6.0;
double lieferantenskonto = 0.0;
double bezugskosten = 780.0;
double gemeinkostenzuschlag = 80.0;
double gewinnaufschlag = 43.0;
double kundenskonto = 0.0;
double verkaufsprovision = 0.0;
double kundenrabatt = 12.0;
string gesucht = "Bruttoverkaufspreis";


Dictionary<string, double> werte = new Dictionary<string, double>
{
    { nameof(mwstsatz), mwstsatz },
    { nameof(bruttoeinkaufspreis), bruttoeinkaufspreis },
    { nameof(lieferantenrabatt), lieferantenrabatt },
    { nameof(lieferantenskonto), lieferantenskonto },
    { nameof(bezugskosten), bezugskosten },
    { nameof(gemeinkostenzuschlag), gemeinkostenzuschlag },
    { nameof(gewinnaufschlag), gewinnaufschlag },
    { nameof(kundenskonto), kundenskonto },
    { nameof(verkaufsprovision), verkaufsprovision },
    { nameof(kundenrabatt), kundenrabatt }
};

foreach (KeyValuePair<string, double> wert in werte)
{
    Console.WriteLine($"{wert.Key}: {wert.Value}");
}


double nettoeinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneNettoEinkaufspreis(bruttoeinkaufspreis,
    mwstsatz);
Console.WriteLine($"nettoeinkaufspreis: {nettoeinkaufspreis:F2}");

double zieleinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneZieleinkaufspreis(nettoeinkaufspreis,
    lieferantenrabatt);
Console.WriteLine($"zieleinkaufspreis: {zieleinkaufspreis:F2}");

double bareinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneBareinkaufspreis(zieleinkaufspreis,
    lieferantenskonto);
Console.WriteLine($"bareinkaufspreis: {bareinkaufspreis:F2}");

double bezugspreis = Preiskalkulator.Preiskalkulator.BerechneBezugspreis(bareinkaufspreis,
    bezugskosten);
Console.WriteLine($"bezugspreis: {bezugspreis:F2}");

double selbstkostenpreis = Preiskalkulator.Preiskalkulator.BerechneSelbstkostenpreis(bezugspreis, gemeinkostenzuschlag );
Console.WriteLine($"selbstkostenpreis: {selbstkostenpreis:F2}");

double barverkaufspreis = Preiskalkulator.Preiskalkulator.BerechneBarverkaufspreis(selbstkostenpreis, gewinnaufschlag);
Console.WriteLine($"barverkaufspreis: {barverkaufspreis:F2}");

double zielverkaufspreis =
    Preiskalkulator.Preiskalkulator.BerechneZielverkaufspreis(barverkaufspreis, kundenskonto, verkaufsprovision);
Console.WriteLine($"zielverkaufspreis: {zielverkaufspreis:F2}");

double nettoverkaufspreis =
    Preiskalkulator.Preiskalkulator.BerechneNettoverkaufspreis(zielverkaufspreis, kundenrabatt);
Console.WriteLine($"nettoverkaufspreis: {nettoverkaufspreis:F2}");

double bruttoverkaufspreis =
    Preiskalkulator.Preiskalkulator.BerechneBruttoverkaufspreis(nettoverkaufspreis, mwstsatz);
Console.WriteLine($"bruttoverkaufspreis: {bruttoverkaufspreis:F2}");
