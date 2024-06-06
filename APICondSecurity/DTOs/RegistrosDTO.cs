using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class RegistrosDTO
    {
        public int IdRegistros { get; set; }
        [Required]
        public DateTime? DataHoraEntrada { get; set; }
        [Required]
        public DateTime? DataHoraSaida { get; set; }

        [Required]
        [StringLength(7)]
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public string Placa { get; set; }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.

        public int? IdVeiculoUsuario { get; set; }

        [Required]
        public int IdPortao { get; set; }

        public int? IdVeiculoTerceiro { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int? IdVeiculo { get; set; }

    }
}
