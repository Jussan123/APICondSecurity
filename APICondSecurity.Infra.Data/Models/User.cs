namespace APICondSecurity.Infra.Data.Models
{
    public class User
    {
        public int Id_user { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
        public byte[] CpfHash { get; set; }
        public byte[] CpfSalt { get; set; }
        public string Telefone { get; private set; }
        public string Situacao { get; private set; }

        public User() { }
   
        public User(int id_user, string name, string email, byte[] cpfHash)
        {
            this.Id_user = id_user;
            this.Name = name;
            this.Email = email;
            this.CpfHash = cpfHash;
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
