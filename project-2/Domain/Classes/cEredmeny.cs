namespace Domain;

public class cEredmeny : cEntity
{
    public long MintaId { get; set; }
    public virtual cMinta Minta { get; set; }
    public long? ParKod { get; set; }
    public long? Megyseg { get; set; }
    public string Ertek { get; set; }
    public float? AlsoMh { get; set; }
    public float? MaxRange { get; set; }
    public float? ErtekHozzarendelt { get; set; }
    public virtual cParameter? Parameter { get; set; }
    public virtual cMertekegyseg? Mertekegyseg { get; set; }

}