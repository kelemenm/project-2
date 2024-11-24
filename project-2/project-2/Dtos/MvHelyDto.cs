using System.ComponentModel.DataAnnotations;

namespace project_2.Dtos
{
    public class MvHelyDto
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string? MvhKod { get; set; }
        [Required]
        [MaxLength(75)]
        public string? NevSajat { get; set; }
        [Required]
        [MaxLength(75)]
        public string? NevTeljes { get; set; }
        [Required]
        [MaxLength(75)]
        public string? VizBazis { get; set; }
        [Required]
        [MaxLength(75)]
        public string? NevRovid { get; set; }
        [Required]
        [MaxLength(75)]
        public string? Telepules { get; set; }
        [Required]
        [MaxLength(75)]
        public string? Tipus { get; set; }
        [MaxLength(75)]
        public string? HumviRegiNev { get; set; }
        public float? GPS_N_Y { get; set; }
        public float? GPS_E_X { get; set; }


    }
}
