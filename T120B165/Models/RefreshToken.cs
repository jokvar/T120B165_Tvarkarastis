using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T120B165.Models
{
    public class RefreshToken
    {
        public string Username { get; set; }
        public string TokenString { get; set; }

        public DateTime ExpiresAt { get; set; }

        public RefreshToken(string Username, string TokenString, DateTime ExpiresAt)
        {
            this.Username = Username;
            this.TokenString = TokenString;
            this.ExpiresAt = ExpiresAt;
        }
    }
}
