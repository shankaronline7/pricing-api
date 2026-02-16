using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJwtTokenService
    {
        (string Token, DateTime Expiry) GenerateToken(long userId, string username);
        void ValidateTokenManually(string token);
    }

}
