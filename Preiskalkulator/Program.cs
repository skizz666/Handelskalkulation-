

double mwstsatz = 19.0;
double bruttoeinkaufspreis = 3100.0;
double lieferantenrabatt = 6.0;
double lieferantenskonto = 0.0;

double nettoeinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneNettoEinkaufspreis(bruttoeinkaufspreis,
    mwstsatz);

double zieleinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneZieleinkaufspreis(nettoeinkaufspreis,
    lieferantenrabatt);

double bareinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneBareinkaufspreis(zieleinkaufspreis,
    lieferantenskonto);

Console.WriteLine(bareinkaufspreis);
