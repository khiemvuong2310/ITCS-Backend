using FSCMS.Core.Entities;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    /// <summary>
    /// Service for managing roles with Memory Cache for performance optimization
    /// Roles are cached since they rarely change (only 6 predefined roles)
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly ILogger<RoleService> _logger;
        private const string ROLES_CACHE_KEY = "all_roles";
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromHours(24);

        public RoleService(
            IUnitOfWork unitOfWork,
            IMemoryCache cache,
            ILogger<RoleService> logger)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// Get all roles from cache or database
        /// Cache for 24 hours since roles rarely change
        /// </summary>
        public async Task<List<Role>> GetAllRolesAsync()
        {
            try
            {
                // Try to get from cache
                if (_cache.TryGetValue(ROLES_CACHE_KEY, out List<Role> cachedRoles))
                {
                    _logger.LogInformation("Roles retrieved from cache");
                    return cachedRoles;
                }

                // If not in cache, load from database
                _logger.LogInformation("Roles not in cache, loading from database");
                var roles = await _unitOfWork.Repository<Role>()
                    .AsQueryable()
                    .Where(r => !r.IsDelete)
                    .OrderBy(r => r.Id)
                    .ToListAsync();

                // Store in cache with 24-hour expiration
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheExpiration,
                    Priority = CacheItemPriority.High // Roles are important, keep in cache
                };

                _cache.Set(ROLES_CACHE_KEY, roles, cacheOptions);
                _logger.LogInformation($"Cached {roles.Count} roles for {CacheExpiration.TotalHours} hours");

                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles");
                throw;
            }
        }

        /// <summary>
        /// Get role by ID from cache
        /// </summary>
        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            try
            {
                var roles = await GetAllRolesAsync();
                return roles.FirstOrDefault(r => r.Id == roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving role with ID {roleId}");
                throw;
            }
        }

        /// <summary>
        /// Get role by name from cache
        /// </summary>
        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(roleName))
                    return null;

                var roles = await GetAllRolesAsync();
                return roles.FirstOrDefault(r => r.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving role with name {roleName}");
                throw;
            }
        }

        /// <summary>
        /// Clear roles cache - call this when roles are updated
        /// </summary>
        public void ClearCache()
        {
            _cache.Remove(ROLES_CACHE_KEY);
            _logger.LogInformation("Roles cache cleared");
        }

        /// <summary>
        /// Refresh roles cache by clearing and reloading
        /// </summary>
        public async Task RefreshCacheAsync()
        {
            try
            {
                ClearCache();
                await GetAllRolesAsync();
                _logger.LogInformation("Roles cache refreshed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing roles cache");
                throw;
            }
        }
    }
}

