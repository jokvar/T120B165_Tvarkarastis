using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T120B165.Models
{
    public class LoginForm
    {
        public string Grant_Type { get; set; }
        public string Client_Id { get; set; }
        public string Client_Secret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Refresh_Token { get; set; }

    }
}
