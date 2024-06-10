using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class LayoutUnificadoPlacaTerceiroDTO
    {
        [Required]
        [StringLength(255)]
        public string Placa { get; set; }

        public int IdPortao { get; set; }

        public int IdPermissao { get; set; }

    }
}
