using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Authentication
{
    public class UserToken
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public double ExpiresIn { get; set; }
    }
}
