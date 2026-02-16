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

        public LoginCommandHandler(
            IUnitOfWork unitOfWork,
            IJwtTokenService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User
                .GetByUsernameAsync(request.Username);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid username");

            if (user.Password != request.Password)
                throw new UnauthorizedAccessException("Invalid password");

            if (user.Status != UserStatus.Active)
                throw new UnauthorizedAccessException("User inactive");

            var token = _jwtService.GenerateToken((int)user.Id, user.Username);

            _jwtService.ValidateTokenManually(token.Token);


            return new LoginResponseDto
            {
                Token = token.Token,
                Expiry = token.Expiry
            };
        }
    }


}
