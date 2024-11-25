using System.ComponentModel.DataAnnotations;

namespace project_2.Dtos
{
    public class HUMVIfelelosDto
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string? Felelos { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Nev { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Cim { get; set; }

    }
}
