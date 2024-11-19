namespace Domain;

public class cAkkrMintavetel : cEntity
{
    public string AkkrMintavetelStatusz { get; set; }
    public string Leiras { get; set; }

    public virtual ICollection<cMinta> Minta { get; set; }
}