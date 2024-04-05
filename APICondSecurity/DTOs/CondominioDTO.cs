using APICondSecurity.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class CondominioDTO
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(1)]
        public char Situacao { get; set; }
    }
}