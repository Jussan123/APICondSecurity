using APICondSecurity.Models;
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
        public virtual Notificacao IdNotificacaoNavigation { get; set; }
        public virtual ICollection<VeiculoTerceiro> VeiculoTerceiro { get; set; } = new List<VeiculoTerceiro>();
    }
}
