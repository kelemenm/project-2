namespace Domain;

public class Minta : Entity
{
    public string labormintakod { get; set; }
    public string Modulkod { get; set; }
    public string Felelos { get; set; }
    public string MvTipus { get; set; }
    public DateTime MvDatum { get; set; }
    public string Labor { get; set; }
    public string Labakkrszam { get; set; }
    public DateTime Mintaatvetel { get; set; }
    public DateTime VizsgalatKezdete { get; set; }
    public DateTime VizsgalatVege { get; set; }
    public string MvOke { get; set; }
    public string MvhKod { get; set; }
    public string MvHely { get; set; }
    public string AkkrMintavetel { get; set; }
    public string Mintavevo { get; set; }
    public string MvAkkrSzam { get; set; }
    public string HUMVIexport { get; set; }
}