using Application.DTOs.UserManagement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.Login
{
    using Application.Interfaces;
    using MediatR;
    using Pricing.Application.Common.Interfaces;
    using Pricing.Domain.Constants;

    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtService;
        private readonly IPasswordService _passwordService;


        public LoginCommandHandler(
            IUnitOfWork unitOfWork,
            IJwtTokenService jwtService,
            IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _passwordService = passwordService;

        }

        public async Task<LoginResponseDto> Handle(
     LoginCommand request,
     CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User
                .GetByUsernameAsync(request.Username);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid username");

            var isValid = _passwordService.VerifyPassword(
                user.PasswordHash,
                request.Password);

            if (!isValid)
                throw new UnauthorizedAccessException("Invalid credentials");

            if (user.Status != UserStatus.Active)
                throw new UnauthorizedAccessException("User inactive");

            // ✅ ROLE-BASED TOKEN GENERATION
            var token = _jwtService.GenerateToken(
                (int)user.Id,
                user.Username,
                user.RoleId,
                user.Role.RoleName
            );

            return new LoginResponseDto
            {
                Token = token.Token,
                Expiry = token.Expiry
            };
        }

    }
}



