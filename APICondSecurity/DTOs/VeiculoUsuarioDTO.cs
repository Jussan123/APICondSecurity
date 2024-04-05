using APICondSecurity.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class VeiculoUsuarioDTO
    {

        [Required]
        [StringLength(255)]
        public string Placa { get; set; }
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int? IdVeiculo { get; set; }

        [Required]
        public int IdRfid { get; set; }

        public virtual Rfid IdRf { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
