using APICondSecurity.Models;
using System.ComponentModel.DataAnnotations.Schema;
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
