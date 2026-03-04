using Application.Common.Interfaces.Data;
using Application.Interfaces;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public AuthorizationService(
            IAuthorizationRepository repository,
            IMemoryCache cache,
            IConfiguration configuration)
        {
            _repository = repository;
            _cache = cache;
            _configuration = configuration;
        }

        public async Task<bool> HasPermissionAsync(int roleId, string permissionCode)
        {
            string cacheKey = $"role_permissions_{roleId}";

            if (!_cache.TryGetValue(cacheKey, out List<string>? permissions))
            {
                permissions = await _repository.GetPermissionsByRoleIdAsync(roleId);

                var cacheMinutes = _configuration.GetValue<int>("AuthorizationCacheMinutes");

                if (cacheMinutes <= 0)
                    cacheMinutes = 30;

                _cache.Set(cacheKey, permissions, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheMinutes),
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                });
            }

            return permissions != null &&
                   permissions.Any(p =>
                       p.Equals(permissionCode, StringComparison.OrdinalIgnoreCase));
        }
    }
}