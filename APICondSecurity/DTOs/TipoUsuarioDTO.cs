using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class TipoUsuarioDTO
    {
        public int IdTipoUsuario { get; set; }
        [Required]
        [StringLength(15)]
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public string Tipo { get; set; }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    }
}
