using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class TipoUsuarioDTO
    {
        [Required]
        [StringLength(15)]
        public string Tipo { get; set; }
    }
}
