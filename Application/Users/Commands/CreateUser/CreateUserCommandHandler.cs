using Application.Common.Interfaces.Data;
using Domain.Entities.UserManagement;
using MediatR;
using Microsoft.AspNetCore.Identity;   // ✅ Only this one
using Pricing.Application.Common.Interfaces;
using Pricing.Domain.Constants;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher<users> _hasher = new();

        public CreateUserCommandHandler(
            IUserRepository userRepo,
            IUnitOfWork unitOfWork)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            if (await _userRepo.UsernameExistsAsync(request.Username))
                throw new Exception("Username already exists");

            var user = new users
            {
                Username = request.Username,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                EmailId = request.EmailId,
                Status = (UserStatus)request.Status,
                CreatedDate = DateTime.UtcNow
            };

            user.Password = _hasher.HashPassword(user, request.Password);

            await _userRepo.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
