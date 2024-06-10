using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class VeiculoTerceiroDTO
    {
        public int IdVeiculoTerceiro { get; set; }

        [Required]
        [StringLength(255)]
        public string Placa { get; set; }
        public int IdVeiculo { get; set; }
        [Required]
        public int IdUsuario { get; set; }
    }
}
