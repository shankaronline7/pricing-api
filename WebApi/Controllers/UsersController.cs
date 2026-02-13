using Application.DTOs.UserManagement;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeactivateUser;
using Application.Users.Commands.UpdateUser;
using MediatR;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetUsersQuery()));
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            return Ok(await Mediator.Send(new GetUsersQuery()));
        }
    }

}
