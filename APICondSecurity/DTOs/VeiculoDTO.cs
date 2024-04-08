using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class VeiculoDTO
    {

        [Required]
        [StringLength(255)]
        public string Placa { get; set; }

        [Required]
        [StringLength(20)]
        public string Marca { get; set; }

        [Required]
        [StringLength(20)]
        public string Modelo { get; set; }

        [Required]
        [StringLength(20)]
        public string Cor { get; set; }

        [Required]
        public int Ano { get; set; }
    }
}
