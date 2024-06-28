using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICondSecurity.Infra.Data.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public int UserId { get; set; }

        public bool TermoAceite { get; set; }

        public string Cpf { get; set; }

        public string Name { get; set; }
        public string CondominioName { get; set; }

        

    }
}