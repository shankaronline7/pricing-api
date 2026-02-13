using Application.DTOs.UserManagement;
using MediatR;
using System.Collections.Generic;

public class GetUsersQuery : IRequest<List<UserListDto>>
{

}
