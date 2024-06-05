using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class VeiculoUsuarioDTO
    {
        public int IdVeiculoUsuario { get; set; }

        [Required]
        [StringLength(255)]
        public string Placa { get; set; }
        public int IdUsuario { get; set; }
        public int IdRfid { get; set; }
    }
}
