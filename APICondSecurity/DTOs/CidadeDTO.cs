using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class CidadeDTO
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        public int CidadeIbge { get; set; }
    }
}
