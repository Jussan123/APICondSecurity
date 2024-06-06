using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.Application.DTOs
{
    public class VeiculoTerceiroDTO
    {
        public int IdVeiculoTerceiro { get; set; }

        [Required]
        [StringLength(255)]
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public string Placa { get; set; }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        [Required]
        public int? IdNotificacao { get; set; }

        public int? IdPermissao { get; set; }

        public int? IdVeiculo { get; set; }
        [Required]
        public int? IdUsuario { get; set; }
    }
}
