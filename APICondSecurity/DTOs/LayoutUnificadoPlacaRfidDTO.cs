using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class LayoutUnificadoPlacaRfidDTO
    {
        [Required]
        [StringLength(255)]
        public string Placa { get; set; }
        
        //public string Numero { get; set; }

        public int IdPortao { get; set; }

    }
}
