using APICondSecurity.Models;
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
        [MaxLength(1)]
        public char? Tipo { get; set; }

        [Required]
        [StringLength(255)]
        public string Imagem { get; set; }

        [Required]
        [MaxLength(1)]
        public char? Situacao { get; set; }

        [Required]
        public int IdUsuario { get; set; }
    }
}
