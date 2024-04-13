using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Senha { get; set; }

        [StringLength(255)]
        public string Telefone { get; set; }

        [StringLength(2)]
        public string Situacao { get; set; }
    }
}
