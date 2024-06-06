using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class UfDTO
    {
        public int IdUf { get; set; }
        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [StringLength(2)]
        public string Sigla { get; set; }
    }
}
