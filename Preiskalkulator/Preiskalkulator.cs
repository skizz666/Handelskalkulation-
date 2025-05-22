namespace Preiskalkulator;

public class Preiskalkulator
{
    public static double BerechneNettoEinkaufspreis(double bruttoeinkaufspreis, double mehrwertsteuersatz)
    {
        double ergebnis = bruttoeinkaufspreis / (1 + (mehrwertsteuersatz / 100.0));
        return ergebnis;
    }

    public static double BerechneZieleinkaufspreis(double nettoeinkaufspreis, double lieferantenrabatt)
    {
        double ergebnis = nettoeinkaufspreis * (1.0 - (lieferantenrabatt / 100.0));
        return ergebnis;
    }
    
    public static double BerechneBareinkaufspreis(double zieleinkaufspreis, double lieferantenskonto)
    {
        double ergebnis = zieleinkaufspreis * (1.0 - (lieferantenskonto / 100.0));
        return ergebnis;
    }

    public static double BerechneBezugspreis(double bareinkaufspreis, double bezugskosten)
    {
        double ergebnis = bareinkaufspreis + bezugskosten;
        return ergebnis;
    }

    public static double BerechneSelbstkostenpreis(double bezugspreis, double gemeinkostenzuschlag)
    {
        double ergebnis = bezugspreis * (1.0 - (gemeinkostenzuschlag / 100.0));
        return ergebnis;
    }

    public static double BerechneBarverkaufspreis(double selbstkostenpreis, double gewinnaufschlag)
    {
        double ergebnis = selbstkostenpreis * (1.0 - (gewinnaufschlag / 100.0));
        return ergebnis;
    }

    public static double BerechneZielverkaufspreis(double barverkaufspreis, double kundenskonto,
        double verkaufsprovision)
    {
        double ergebnis = barverkaufspreis * (1.0 - ((kundenskonto + barverkaufspreis) / 100.0));
        return ergebnis;
    }
}