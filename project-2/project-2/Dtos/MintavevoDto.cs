using System.ComponentModel.DataAnnotations;

namespace project_2.Dtos
{
    public class MintavevoDto
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string? MintavevoAzonosito { get; set; }
        [Required]
        [MaxLength(15)]
        public string? MvAkkrSzam { get; set; }
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
}
