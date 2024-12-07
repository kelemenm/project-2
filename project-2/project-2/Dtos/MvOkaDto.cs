namespace project_2.Dtos;

public class MvOkaDto : BaseDto
{
    [Required]
    [MaxLength(10)]
    public string? MvOk { get; set; }
    [Required]
    [MaxLength(15)]
    public string? Leiras { get; set; }
}
