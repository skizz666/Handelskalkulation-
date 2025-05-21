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
}