using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class ResidenciaDTO
    {
        public int IdResidencia { get; set; }
        public int Numero { get; set; }

        [StringLength(20)]
        public string Bloco { get; set; }

        [StringLength(20)]
        public string Quadra { get; set; }

        [StringLength(50)]
        public string Rua { get; set; }
        public int IdCondominio { get; set; }
    }
}
