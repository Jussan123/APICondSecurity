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
        [StringLength(2)]
        public string Situacao { get; set; }

        public int IdEndereco { get; set; }
    }
}