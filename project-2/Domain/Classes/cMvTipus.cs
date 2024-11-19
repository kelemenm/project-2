namespace Domain;

public class cMvTipus : cEntity
{
    public string MvTipusNev { get; set; }
    public string Leiras { get; set; }

    public virtual ICollection<cMinta> Minta { get; set; }
}