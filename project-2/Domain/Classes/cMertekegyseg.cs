namespace Domain;

public class cMertekegyseg : cEntity
{
    public string Megyseg { get; set; }
    public string HumviLeiras { get; set; }
    public string SajatLeiras { get; set; }

    public virtual ICollection<cEredmeny> Eredmeny { get; set; }
}