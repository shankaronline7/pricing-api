using Pricing.Domain.Common;
using Pricing.Domain.Constants;

namespace Application.DTOs.UserManagement
{
    public class CreateUserDto: BaseAuditableEntity
    {
        
            public string Username { get; set; } = null!;
            public string Firstname { get; set; } = null!;
            public string Lastname { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string EmailId { get; set; } = null!;
            public UserStatus Status { get; set; }

    }
}

