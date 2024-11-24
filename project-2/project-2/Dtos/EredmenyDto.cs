using Domain;
using System.ComponentModel.DataAnnotations;

namespace project_2.Dtos
{
    public class EredmenyDto
    {
        public long? Id { get; set; }
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
}
