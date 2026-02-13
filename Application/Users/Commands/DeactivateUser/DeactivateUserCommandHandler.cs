using Application.Common.Interfaces.Data;
using MediatR;
using Pricing.Application.Common.Interfaces;
using Pricing.Domain.Constants;

namespace Application.Users.Commands.DeactivateUser
{
    public class DeactivateUserCommandHandler
        : IRequestHandler<DeactivateUserCommand, bool>
    {
        private readonly IUserRepository _repo;
        private readonly IUnitOfWork _uow;

        public DeactivateUserCommandHandler(
            IUserRepository repo,
            IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task<bool> Handle(
            DeactivateUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _repo.GetByIdAsync(request.UserId);
            if (user == null) return false;

            user.Status = UserStatus.Inactive;

            await _uow.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
