namespace Domain;

public class cHUMVImodul : cEntity
{
    public string? ModulKod { get; set; }
    public string? Leiras { get; set; }

    public virtual ICollection<cMinta> Minta { get; set; }
}