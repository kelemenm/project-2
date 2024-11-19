namespace Domain;

public class cHUMVIfelelos : cEntity
{
    public string Felelos { get; set; }
    public string Nev { get; set; }
    public string Cim { get; set; }

    public virtual ICollection<cMinta> Minta { get; set; }
}