using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.Infra.Data.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O E-mail é obrigatório!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "A Senha é obrigatória!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
