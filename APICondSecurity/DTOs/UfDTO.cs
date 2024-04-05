using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class UfDTO
    {
        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [StringLength(2)]
        public string Sigla { get; set; }
    }
}
