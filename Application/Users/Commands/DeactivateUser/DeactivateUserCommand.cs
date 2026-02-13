using MediatR;

namespace Application.Users.Commands.DeactivateUser
{
    public class DeactivateUserCommand : IRequest<bool>
    {
        public long UserId { get; set; }
    }
}
