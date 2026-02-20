using Application.Common.Interfaces.Data;
using Domain.Entities.UserManagement;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Pricing.Infrastructure.Persistence;
using Pricing.Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository
        : RepositoryBase<users>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(users entity)
        {
            await _context.Set<users>().AddAsync(entity);
        }

        public async Task<List<users>> GetAllAsync()
        {
            return await _context.users
                .Include(u => u.Role)
                .ToListAsync();
        }


        public async Task<users?> GetByIdAsync(long userId)
        {
            return await _context.Set<users>()
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<users> GetByUsernameAsync(string username)
        {
            return await _context.Set<users>()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(x => x.Username == username);
        }


        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Set<users>()
                .AnyAsync(x => x.Username == username);
        }
    }
}
