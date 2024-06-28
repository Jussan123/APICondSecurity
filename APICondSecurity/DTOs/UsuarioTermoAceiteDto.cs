using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class UserTermoAceiteDto
    {
        [Required]
        public int IdUser { get; set; }
    }
}
