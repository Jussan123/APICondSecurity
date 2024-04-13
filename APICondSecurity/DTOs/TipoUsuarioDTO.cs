using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class TipoUsuarioDTO
    {
        public int IdTipoUsuario { get; set; }
        [Required]
        [StringLength(15)]
        public string Tipo { get; set; }
    }
}
