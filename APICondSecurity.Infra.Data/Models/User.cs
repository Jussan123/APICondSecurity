using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICondSecurity.Infra.Data.Models
{
    internal class User
    {
        public int id_user {  get; private set; }
        public string name { get; private set; }
        public string email { get; private set; }
        public byte[] senhaHash { get; private set; }
        public byte[] senhaSalt { get; private set; }

        public User(int id_user, string name, string email)
        {
            this.id_user = id_user;
            this.name = name;
            this.email = email;
        }

        public User(string name, string email)
        {
            this.name = name;
            this.email = email;
        }

        public void alterarSenha(byte[] senhaHash, byte[] senhaSalt)
        {
            this.senhaHash = senhaHash;
            this.senhaSalt = senhaSalt;
        }

        
    }
}
