using MediatR;
using Application.DTOs.UserManagement;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public UpdateUserDto Dto { get; set; }
    }
}
