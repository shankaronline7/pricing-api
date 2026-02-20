using MediatR;
using Application.DTOs.UserManagement;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<long>
    {
        public int RoleId { get; set; }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
    }


}
