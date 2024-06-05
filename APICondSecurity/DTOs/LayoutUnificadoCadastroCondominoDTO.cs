using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class LayoutUnificadoCadastroCondominoDTO
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(2)]
        public string Situacao { get; set; }
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
        [Required]
        [StringLength(30)]
        public string NomeUf { get; set; }
        [Required]
        [StringLength(2)]
        public string Sigla { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCidade { get; set; }
        [Required]
        public int CidadeIbge { get; set; }
    }
}
