using Domain.Entities.UserManagement;

namespace Application.Common.Interfaces.Data;

public interface IUserRepository
{
    Task AddAsync(users entity);

    Task<List<users>> GetAllAsync();

    Task<users?> GetByIdAsync(long userId);

    Task<bool> UsernameExistsAsync(string username);
}
