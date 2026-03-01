using Application.Common.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Pricing.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetPermissionsByRoleIdAsync(int roleId)
        {
            return await (
                from rp in _context.RolePermissions
                join ap in _context.ApiPermissions
                    on rp.PermissionId equals ap.PermissionId
                where rp.RoleId == roleId
                select ap.PermissionCode
            ).ToListAsync();
        }
    }
}

