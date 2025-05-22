

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

double nettoeinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneNettoEinkaufspreis(bruttoeinkaufspreis,
    mwstsatz);

double zieleinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneZieleinkaufspreis(nettoeinkaufspreis,
    lieferantenrabatt);

double bareinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneBareinkaufspreis(zieleinkaufspreis,
    lieferantenskonto);

double bezugspreis = Preiskalkulator.Preiskalkulator.BerechneBezugspreis(bareinkaufspreis,
    bezugskosten);

double selbstkostenpreis = Preiskalkulator.Preiskalkulator.BerechneSelbstkostenpreis(bezugspreis,gemeinkostenzuschlag );

double barverkaufspreis = Preiskalkulator.Preiskalkulator.BerechneBarverkaufspreis(selbstkostenpreis, gewinnaufschlag);

Console.WriteLine(barverkaufspreis);
