using System.ComponentModel.DataAnnotations;

namespace project_2.Dtos
{
    public class MintaDto
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string? LaborMintaKod { get; set; }
        [Required]
        public long? ModulKod { get; set; }
        [Required]
        public long? Felelos { get; set; }
        [Required]
        public long? MvTipus { get; set; }
        [Required]
        public DateTime MvDatum { get; set; }
        [Required]
        public long? Labor { get; set; }
        [Required]
        public DateTime MintaAtvetel { get; set; }
        [Required]
        public DateTime VizsgalatKezdete { get; set; }
        [Required]
        public DateTime VizsgalatVege { get; set; }
        [Required]
        public long? MvOk { get; set; }
        [MaxLength(255)]
        public string? MvOkaEgyeb { get; set; }
        [Required]
        public long? MvhKod { get; set; }
        [MaxLength(255)]
        public string? MvHely { get; set; }
        public long? AkkrMintavetel { get; set; }
        public long? Mintavevo { get; set; }
        [Required]
        public bool HUMVIexport { get; set; }


    }
}
