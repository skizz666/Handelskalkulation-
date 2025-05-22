

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
Console.WriteLine(nettoeinkaufspreis);

double zieleinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneZieleinkaufspreis(nettoeinkaufspreis,
    lieferantenrabatt);
Console.WriteLine(zieleinkaufspreis);

double bareinkaufspreis = Preiskalkulator.Preiskalkulator.BerechneBareinkaufspreis(zieleinkaufspreis,
    lieferantenskonto);
Console.WriteLine(bareinkaufspreis);

double bezugspreis = Preiskalkulator.Preiskalkulator.BerechneBezugspreis(bareinkaufspreis,
    bezugskosten);
Console.WriteLine(bezugspreis);

double selbstkostenpreis = Preiskalkulator.Preiskalkulator.BerechneSelbstkostenpreis(bezugspreis, gemeinkostenzuschlag );
Console.WriteLine(selbstkostenpreis);

double barverkaufspreis = Preiskalkulator.Preiskalkulator.BerechneBarverkaufspreis(selbstkostenpreis, gewinnaufschlag);
Console.WriteLine(barverkaufspreis);

double zielverkaufspreis =
    Preiskalkulator.Preiskalkulator.BerechneZielverkaufspreis(barverkaufspreis, kundenskonto, verkaufsprovision);
Console.WriteLine(zielverkaufspreis);


