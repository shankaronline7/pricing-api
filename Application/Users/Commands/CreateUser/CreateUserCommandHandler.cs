using Application.Common.Interfaces.Data;
using Domain.Entities.UserManagement;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Pricing.Application.Common.Interfaces;
using Pricing.Domain.Constants;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
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

        // --- Combined FluentValidation class inside the handler ---
        private class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
        {
            public CreateUserCommandValidator()
            {
                RuleFor(x => x.Username)
                    .NotEmpty().WithMessage("Username is required")
                    .MaximumLength(100).WithMessage("Username cannot exceed 100 characters");

                RuleFor(x => x.Firstname)
                    .NotEmpty().WithMessage("Firstname is required")
                    .MaximumLength(100);

                RuleFor(x => x.Lastname)
                    .NotEmpty().WithMessage("Lastname is required")
                    .MaximumLength(100);

                RuleFor(x => x.EmailId)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Invalid email format")
                    .MaximumLength(150);

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required")
                    .MinimumLength(6).WithMessage("Password must be at least 6 characters");

                RuleFor(x => x.Status)
                    .IsInEnum().WithMessage("Status is required");

                RuleFor(x => x.CreatedBy)
                    .NotEmpty().WithMessage("CreatedBy is required")
                    .MaximumLength(100);
            }
        }

        // --- Handler method ---
        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Validate request
            var validator = new CreateUserCommandValidator();
            ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            // 2️⃣ Check if username already exists
            if (await _userRepo.UsernameExistsAsync(request.Username))
                throw new Exception("Username already exists");

            // 3️⃣ Create user object
            var user = new users
            {
                Username = request.Username,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                EmailId = request.EmailId,
                Status = (UserStatus)request.Status,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = request.CreatedBy
            };

            // 4️⃣ Hash password
            user.Password = _hasher.HashPassword(user, request.Password);

            // 5️⃣ Save to database
            await _userRepo.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
