namespace Domain;

public class VizsgaloLabor : Entity
{
    public string Labor { get; set; }
    public string Labakkrszam { get; set; }
    public string Nev { get; set; }
    public string Cim { get; set; }
    public DateTime ErvKezdete { get; set; }
    public DateTime ErvVege { get; set; }
}