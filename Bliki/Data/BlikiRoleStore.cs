using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace Bliki.Data
{
    public class BlikiRoleStore : IRoleStore<IdentityRole>
    {
        public BlikiRoleStore()
        {
            _saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "RoleStore.json");
            _roles = new List<IdentityRole>();
            LoadRoles();
        }


        public Task<IdentityResult> CreateAsync(IdentityRole role,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public Task<IdentityResult> DeleteAsync(IdentityRole role,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            // nothing to do.
        }


        public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                LoadRoles();
                return _roles.FirstOrDefault(u => u.Id == roleId);
            }, cancellationToken);
        }


        public Task<IdentityRole> FindByNameAsync(string normalizedRoleName,
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                LoadRoles();
                return _roles.FirstOrDefault(u => u.NormalizedName == normalizedRoleName);
            }, cancellationToken);
        }


        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role,
            CancellationToken cancellationToken)
        {
            return Task.Run(() => { return role.NormalizedName; });
        }


        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() => { return role.Id; });
        }


        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() => { return role.Name; });
        }


        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public Task SetRoleNameAsync(IdentityRole role, string roleName,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public Task<IdentityResult> UpdateAsync(IdentityRole role,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        private void LoadRoles()
        {
            _roles.Clear();
            if (File.Exists(_saveFilePath))
            {
                string json;
                lock (_fileLock)
                {
                    json = File.ReadAllText(_saveFilePath);
                }

                _roles.AddRange(JsonSerializer.Deserialize<IdentityRole[]>(json));
            }
        }


        private readonly object _fileLock = new object();
        private readonly List<IdentityRole> _roles;
        private readonly string _saveFilePath;
    }
}