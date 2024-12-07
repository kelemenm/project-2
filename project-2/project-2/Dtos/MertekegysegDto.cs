namespace project_2.Dtos;

public class MertekegysegDto : BaseDto
{
    [Required]
    [MaxLength(15)]
    public string? Megyseg { get; set; }
    [Required]
    [MaxLength(15)]
    public string? HumviLeiras { get; set; }
    [Required]
    [MaxLength(15)]
    public string? SajatLeiras { get; set; }
}
