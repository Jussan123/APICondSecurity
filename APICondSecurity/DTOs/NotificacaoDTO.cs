using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class NotificacaoDTO
    {
        public int IdNotificacao { get; set; }
        [Required]
        [Precision(6, 0)]
        public TimeOnly DataHora { get; set; }

        [Required]
        public char? Tipo { get; set; }

        [Required]
        [StringLength(255)]
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public string Imagem { get; set; }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.

        [Required]
        public char? Situacao { get; set; }

        public string Mensagem { get; set; }

        [Required]
        public int IdUsuario { get; set; }
    }
}
