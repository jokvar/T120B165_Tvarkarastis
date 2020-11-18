using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T120B165.Models
{
    public interface IRefreshTokenManager
    {
        void Add(RefreshToken refreshToken);
        RefreshToken Get(string token);
        void Remove(RefreshToken refreshToken);
        void RemoveByUsername(string username);
    }
    public class RefreshTokenManager : IRefreshTokenManager
    {
        private List<RefreshToken> refreshTokens;
        private object __lock = new object();
        public RefreshTokenManager()
        {
            refreshTokens = new List<RefreshToken>();
        }
        public void Add(RefreshToken refreshToken)
        {
            lock (__lock)
            {
                refreshTokens.Add(refreshToken);
            }
        }

        public RefreshToken Get(string refresh_token)
        {
            lock (__lock)
            {
                return refreshTokens.Where(rt => rt.TokenString == refresh_token).FirstOrDefault();
            }
        }

        public void Remove(RefreshToken refreshToken)
        {
            lock (__lock)
            {
                refreshTokens.Remove(refreshToken);
            }
        }

        public void RemoveByUsername(string username)
        {
            lock (__lock)
            {
                RefreshToken[] copy = new RefreshToken[refreshTokens.Count];
                refreshTokens.CopyTo(copy);
                foreach (var token in copy)
                {
                    if (token.Username == username)
                    { 
                        refreshTokens.Remove(token);
                        return;
                    }
                }
            }
        }
    }
}
