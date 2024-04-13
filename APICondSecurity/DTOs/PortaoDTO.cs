using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class PortaoDTO
    {
        public int IdPortao { get; set; }
        [Required]
        [StringLength(20)]
        public string Nome { get; set; }
    }
}
