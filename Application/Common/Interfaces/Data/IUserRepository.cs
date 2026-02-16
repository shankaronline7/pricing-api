using Domain.Entities.UserManagement;
using Microsoft.AspNet.Identity;

namespace Application.Common.Interfaces.Data;

public interface IUserRepository
{
    Task AddAsync(users entity);

    Task<List<users>> GetAllAsync();

    Task<users?> GetByIdAsync(long userId);
    Task<users> GetByUsernameAsync(string username);


    Task<bool> UsernameExistsAsync(string username);
}
