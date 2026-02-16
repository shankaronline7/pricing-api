using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserManagement
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }

}
