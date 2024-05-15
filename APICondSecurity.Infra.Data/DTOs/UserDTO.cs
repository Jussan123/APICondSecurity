using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICondSecurity.Infra.Data.DTOs
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string senha { get; set; }

        public string telefone { get; set; }
        public string situacao { get; set; }
    }
}
