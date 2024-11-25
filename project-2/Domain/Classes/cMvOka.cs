namespace Domain;

public class cMvOka : cEntity
{
    public string? MvOk { get; set; }
    public string? Leiras { get; set; }

    public virtual ICollection<cMinta> Minta { get; set; }
}