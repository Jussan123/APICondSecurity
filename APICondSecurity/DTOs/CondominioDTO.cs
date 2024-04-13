using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class CondominioDTO
    {
        public int IdCondominio { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(1)]
        public char Situacao { get; set; }
    }
}