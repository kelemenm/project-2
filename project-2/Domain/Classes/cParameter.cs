namespace Domain;

public class cParameter : cEntity
{
    public string ParKod { get; set; }
    public string HumviLeiras { get; set; }
    public string SajatLeiras { get; set; }
    public string ParamErtek { get; set; }
    public string ParamTip { get; set; }

    public virtual ICollection<cEredmeny> Eredmeny { get; set; }
}