using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.Application.DTOs
{
    public class VeiculoUsuarioDTO
    {
        public int IdVeiculoUsuario { get; set; }

        [Required]
        [StringLength(255)]
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public string Placa { get; set; }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int? IdVeiculo { get; set; }

        [Required]
        public int IdRfid { get; set; }
    }
}
