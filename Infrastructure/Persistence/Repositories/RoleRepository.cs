using Application.Common.Interfaces.Data;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Pricing.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetByIdAsync(int roleId)
        {
            return await _context.Set<Role>()
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
        }
    }

}
