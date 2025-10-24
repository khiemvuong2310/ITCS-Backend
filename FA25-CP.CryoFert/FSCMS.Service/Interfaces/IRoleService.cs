using FSCMS.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    /// <summary>
    /// Service for managing roles with caching support
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Get all roles from cache or database
        /// </summary>
        Task<List<Role>> GetAllRolesAsync();

        /// <summary>
        /// Get role by ID from cache
        /// </summary>
        Task<Role?> GetRoleByIdAsync(int roleId);

        /// <summary>
        /// Get role by name from cache
        /// </summary>
        Task<Role?> GetRoleByNameAsync(string roleName);

        /// <summary>
        /// Clear roles cache (use when roles are updated)
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Refresh roles cache
        /// </summary>
        Task RefreshCacheAsync();
    }
}

