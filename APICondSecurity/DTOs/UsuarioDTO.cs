using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Senha { get; set; }

        [StringLength(255)]
        public string Telefone { get; set; }

        [StringLength(255)]
        public string Cpf { get; set; }

        [StringLength(2)] // Se Situacao é obrigatório, remova o ? para torná-lo não anulável
        public string? Situacao { get; set; } // Anulável, pode ser nulo
    }
}
