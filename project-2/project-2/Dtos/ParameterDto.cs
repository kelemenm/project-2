using System.ComponentModel.DataAnnotations;

namespace project_2.Dtos
{
    public class ParameterDto
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string ParKod { get; set; }
        [Required]
        [MaxLength(75)]
        public string HumviLeiras { get; set; }
        [Required]
        [MaxLength(75)]
        public string SajatLeiras { get; set; }      
        [MaxLength(75)]
        public string ParamErtek { get; set; }
        [MaxLength(25)]
        public string ParamTip { get; set; }

    }
}
