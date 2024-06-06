using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class LayoutUnificadoCadastroUsuarioDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Senha { get; set; }

        [StringLength(255)]
        public string Cpf { get; set; }

        [StringLength(255)]
        public string Telefone { get; set; }

        [StringLength(2)]
        public string? Situacao { get; set; }
        public int Numero { get; set; }

        [StringLength(20)]
        public string Bloco { get; set; }

        [StringLength(20)]
        public string Quadra { get; set; }

        [StringLength(50)]
        public string Rua { get; set; }
        [StringLength(20)]
        public string Tipo { get; set; }
    }
}
