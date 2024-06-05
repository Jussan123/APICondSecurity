using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class EnderecoDTO
    {
        public int IdEndereco { get; set; }
        [Required]
        [StringLength(30)]
        public string Rua { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        [StringLength(30)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(9)]
        public string Cep { get; set; }

        [StringLength(100)]
        public string Complemento { get; set; }
    }
}
