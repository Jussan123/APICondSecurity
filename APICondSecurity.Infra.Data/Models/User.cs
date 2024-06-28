using System.ComponentModel.DataAnnotations.Schema;

namespace APICondSecurity.Infra.Data.Models
{
    public class User
    {
        public int Id_user { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
        public byte[] CpfHash { get; set; }
        public byte[] CpfSalt { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Situacao { get; set; }
        public int IdTipoUsuario { get; set; }
        public bool TermoAceite { get; set; } = false;

        public User() { }
   
        public User(int id_user, string name, string email, byte[] cpfHash, byte[] cpfSalt, int IdTipoUsuario)
        {
            this.Id_user = id_user;
            this.Name = name;
            this.Email = email;
            this.CpfHash = cpfHash;
            this.CpfSalt = cpfSalt;
            this.IdTipoUsuario = IdTipoUsuario;
        }

        public User(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        public void AlterarSenha(byte[] senhaHash, byte[] senhaSalt)
        {
            this.SenhaHash = senhaHash;
            this.SenhaSalt = senhaSalt;
        }

    }
}
