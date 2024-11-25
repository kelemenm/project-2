namespace Domain;

public class cVizsgaloLabor : cEntity
{
    public string? Labor { get; set; }
    public string? LabAkkrSzam { get; set; }
    public string? Nev { get; set; }
    public string? Cim { get; set; }
    public DateTime ErvKezdete { get; set; }
    public DateTime ErvVege { get; set; }

    public virtual ICollection<cMinta> Minta { get; set; }
}