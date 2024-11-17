namespace Domain;

public class Eredmeny : Entity
{
    public long MintaId { get; set; }
    public virtual Minta Minta { get; set; }
    public string Parkod { get; set; }
    public string Megyseg { get; set; }
    public string Ertek { get; set; }
    public float Alsomh { get; set; }
    public float MaxRange { get; set; }
    public float ErtekHozzarendelt { get; set; }
}