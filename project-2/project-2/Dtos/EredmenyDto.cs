namespace project_2.Dtos;

public class EredmenyDto : BaseDto
{
    [Required]
    public long? MintaId { get; set; }
    [Required]
    public long? ParKod { get; set; }
    [Required]
    public long? Megyseg { get; set; }
    [Required]
    [MaxLength(25)]
    public string? Ertek { get; set; }
    public float? AlsoMh { get; set; }
    public float? MaxRange { get; set; }
    public float? ErtekHozzarendelt { get; set; }
}
