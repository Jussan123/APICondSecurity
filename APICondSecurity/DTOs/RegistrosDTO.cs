using APICondSecurity.Models;
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
        public string Placa { get; set; }

        public int? IdVeiculoUsuario { get; set; }

        [Required]
        public int IdPortao { get; set; }

        public int? IdVeiculoTerceiro { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int? IdVeiculo { get; set; }

        public virtual Portao IdPortaoNavigation { get; set; }

        public virtual VeiculoTerceiro IdVeiculoTerceiroNavigation { get; set; }

        public virtual VeiculoUsuario IdVeiculoUsuarioNavigation { get; set; }
    }
}
