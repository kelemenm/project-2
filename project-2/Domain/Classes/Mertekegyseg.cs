namespace Domain;

public class Mertekegyseg : Entity
{
    public string Megyseg { get; set; }
    public string HumviLeiras { get; set; }
    public string SajatLeiras { get; set; }

    public virtual ICollection<Eredmeny> Eredmeny { get; set; }
}