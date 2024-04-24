using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class PermissaoDTO
    {
        public int IdPermissao { get; set; }
        [Required]
        [MaxLength(1)]
        public char Situacao { get; set; }

        [Required]
        public int IdNotificacao { get; set; }
    }
}
