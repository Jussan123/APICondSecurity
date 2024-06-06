using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class RfidDTO
    {
        public int IdRfid { get; set; }
        public int Numero { get; set; }

        [MaxLength(1)]
        public char Situacao { get; set; }

        public int IdCondominio { get; set; }

    }
}
