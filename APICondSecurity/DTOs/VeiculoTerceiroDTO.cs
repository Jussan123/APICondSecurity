using APICondSecurity.Models;
using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class VeiculoTerceiroDTO
    {
        public int IdVeiculoTerceiro { get; set; }

        [Required]
        [StringLength(255)]
        public string Placa { get; set; }
        [Required]
        public int? IdNotificacao { get; set; }

        public int? IdPermissao { get; set; }

        public int? IdVeiculo { get; set; }
        [Required]
        public int? IdUsuario { get; set; }

        public virtual Notificacao IdNotificacaoNavigation { get; set; }

        public virtual Permissao IdPermissaoNavigation { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }

        public virtual ICollection<Registros> Registros { get; set; } = new List<Registros>();

    }
}
