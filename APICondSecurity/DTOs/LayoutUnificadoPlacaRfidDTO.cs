using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class LayoutUnificadoPlacaRfidDTO
    {
        [Required]
        [StringLength(255)]
        public string Placa { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Numero { get; set; }

    }
}
