namespace Domain;

public class Eredmeny : Entity
{
    public long MintaId { get; set; }
    public virtual Minta Minta { get; set; }
    public string? ParKod { get; set; }
    public string? Megyseg { get; set; }
    public string Ertek { get; set; }
    public float? AlsoMh { get; set; }
    public float? MaxRange { get; set; }
    public float? ErtekHozzarendelt { get; set; }
    public virtual Parameter? Parameter { get; set; }
    public virtual Mertekegyseg? Mertekegyseg { get; set; }

}