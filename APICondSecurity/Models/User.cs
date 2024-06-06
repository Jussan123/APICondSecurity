namespace APICondSecurity.Models
{
    public class User
    {
        public int Id_user { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public byte[] SenhaHash { get; private set; }
        public byte[] SenhaSalt { get; private set; }
        public byte[] CpfHash { get; private set; }
        public byte[] CpfSalt { get; private set; }
        public string Telefone { get; private set; }
        public string Situacao { get; private set; }

        public User() { }
        public User(int id_user, string name, string email, byte[] cpfHash, byte[] cpfSalt)
        {
            this.Id_user = id_user;
            this.Name = name;
            this.Email = email;
            this.CpfHash = cpfHash;
            this.CpfSalt = cpfSalt;
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
