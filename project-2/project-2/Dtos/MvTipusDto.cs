namespace project_2.Dtos;

public class MvTipusDto : BaseDto
{
    [Required]
    [MaxLength(10)]
    public string? MvTipusNev { get; set; }
    [Required]
    [MaxLength(50)]
    public string? Leiras { get; set; }
}
