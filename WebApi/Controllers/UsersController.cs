using Application.DTOs.UserManagement;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeactivateUser;
using Application.Users.Commands.Login;
using Application.Users.Commands.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pricing.WebApi.Controllers;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateUserDto dto)
        {
            var result = await Mediator.Send(
                new UpdateUserCommand { Id = id, Dto = dto });

            return result ? Ok() : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(long id)
        {
            var result = await Mediator.Send(new DeactivateUserCommand
            {
                UserId = id
            });

            if (!result)
                return NotFound("User not found");

            return Ok("User deactivated successfully");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetUsersQuery()));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var command = new LoginCommand
            {
                Username = request.Username,
                Password = request.Password
            };

            var result = await Mediator.Send(command);

            return Ok(result);
        }
        [Authorize]
        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await Mediator.Send(new GetUsersQuery());
            return Ok(result);
        }


    }

}
