using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class RfidDTO
    {
        public int IdRfid { get; set; }
        public string Numero { get; set; }

      
        public char Situacao { get; set; }

        public int IdCondominio { get; set; }

    }
}
