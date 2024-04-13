using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class CidadeDTO
    {
        public int IdCidade { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        public int CidadeIbge { get; set; }
    }
}
