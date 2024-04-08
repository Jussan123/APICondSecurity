using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class PortaoDTO
    {
        [Required]
        [StringLength(20)]
        public string Nome { get; set; }
    }
}
