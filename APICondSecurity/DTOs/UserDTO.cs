using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICondSecurity.DTOs
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string senha { get; set; }
    }
}
