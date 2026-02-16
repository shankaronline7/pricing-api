using Application.Common.Interfaces.Data;
using Domain.Entities.UserManagement;
using MediatR;
namespace Pricing.Domain.Constants;
using global::Application.Users.Commands.UpdateUser;
using Microsoft.AspNetCore.Identity;
using Pricing.Application.Common.Interfaces;


    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly PasswordHasher<users> _hasher = new();

        public UpdateUserCommandHandler(
            IUserRepository repo,
            IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task<bool> Handle(
     UpdateUserCommand request,
     CancellationToken cancellationToken)
        {
            var user = await _repo.GetByIdAsync(request.Id);
            if (user == null)
                return false;

            // ✅ Update all fields safely
            user.Username = request.Dto.Username ?? user.Username;
            user.Firstname = request.Dto.Firstname ?? user.Firstname;
            user.Lastname = request.Dto.Lastname ?? user.Lastname;
            user.EmailId = request.Dto.EmailId ?? user.EmailId;
            user.CreatedBy = request.Dto.CreatedBy ?? user.CreatedBy;

        // If Status is nullable in DTO
        if (request.Dto.Status.HasValue)
                user.Status = (Domain.Constants.UserStatus)request.Dto.Status.Value;

            // Update password only if provided
            if (!string.IsNullOrEmpty(request.Dto.Password))
            {
                user.Password = _hasher.HashPassword(user, request.Dto.Password);
            }

            await _uow.SaveChangesAsync(cancellationToken);

            return true;
        }

    }

