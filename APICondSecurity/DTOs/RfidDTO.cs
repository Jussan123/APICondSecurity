using APICondSecurity.Models;
using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class RfidDTO
    {
        public int Numero { get; set; }

        [MaxLength(1)]
        public char Situacao { get; set; }

        public int IdCondominio { get; set; }

        public virtual Condominio IdCondominioNavigation { get; set; }
        public virtual ICollection<VeiculoUsuario> VeiculoUsuario { get; set; } = new List<VeiculoUsuario>();

    }
}
