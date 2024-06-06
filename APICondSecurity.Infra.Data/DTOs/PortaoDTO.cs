using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.Infra.Data.DTOs
{
    public class PortaoDTO
    {
        public int IdPortao { get; set; }
        [Required]
        [StringLength(20)]
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public string Nome { get; set; }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    }
}
