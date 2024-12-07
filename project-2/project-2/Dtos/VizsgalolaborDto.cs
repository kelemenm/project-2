namespace project_2.Dtos;

public class VizsgaloLaborDto : BaseDto
{
    [Required]
    [MaxLength(10)]
    public string? Labor { get; set; }
    [Required]
    [MaxLength(15)]
    public string? LabAkkrSzam { get; set; }
    [Required]
    [MaxLength(150)]
    public string? Nev { get; set; }
    [Required]
    [MaxLength(150)]
    public string? Cim { get; set; }
    [Required]
    public DateTime ErvKezdete { get; set; }
    [Required]
    public DateTime ErvVege { get; set; }
}
