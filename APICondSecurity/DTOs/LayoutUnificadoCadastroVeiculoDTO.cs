using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class LayoutUnificadoCadastroVeiculoDTO
    {
        [Required]
        [StringLength(255)]
        public string Placa { get; set; }

        [StringLength(20)]
        public string Marca { get; set; }

        [StringLength(20)]
        public string Modelo { get; set; }

        [StringLength(20)]
        public string Cor { get; set; }

        public int Ano { get; set; }
        [StringLength(2)]
        public string Situacao { get; set; }
        public int IdUsuario { get; set; }
        public int IdRfid { get; set; }
    }
}
