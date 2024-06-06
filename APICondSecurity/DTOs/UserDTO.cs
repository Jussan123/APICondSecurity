using System.ComponentModel.DataAnnotations.Schema;

namespace APICondSecurity.DTOs
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string Senha { get; set; }
        [NotMapped]
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Situacao { get; set; }
    }
}
