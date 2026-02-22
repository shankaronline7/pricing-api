using Pricing.Domain.Common;

namespace Application.DTOs.UserManagement
{
    public class DeactivateUserDto: BaseAuditableEntity
    {
        public long UserId { get; set; }
    }
}
