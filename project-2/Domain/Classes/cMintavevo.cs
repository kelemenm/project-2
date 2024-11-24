namespace Domain;

public class cMintavevo : cEntity
{
    public string? MintavevoAzonosito { get; set; }
    public string? MvAkkrSzam { get; set; }
    public string? Nev { get; set; }
    public string? Cim { get; set; }
    public DateTime ErvKezdete { get; set; }
    public DateTime ErvVege { get; set; }

    public virtual ICollection<cMinta> Minta { get; set; }
}