using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Bliki.Data
{
    public class BlikiUserStore : IUserStore<BlikiUser>, IUserEmailStore<BlikiUser>,
        IUserPhoneNumberStore<BlikiUser>, IUserTwoFactorStore<BlikiUser>,
        IUserPasswordStore<BlikiUser>, IQueryableUserStore<BlikiUser>, IUserRoleStore<BlikiUser>
    {
        private readonly string _saveFilePath;
        private readonly List<BlikiUser> _users;

        public BlikiUserStore()
        {
            _saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "UserStore.json");
            _users = new List<BlikiUser>();
            LoadUsers();
        }

        public Task<IdentityResult> CreateAsync(BlikiUser user,
            CancellationToken cancellationToken)
        {
            try
            {
                LoadUsers();
                if (!_users.Any())
                {
                    user.Roles = new[] { "admin" };
                }
                if (string.IsNullOrEmpty(user.Email))
                {
                    user.Email = user.UserName;
                }
                if (_users.Any(u => u.Email == user.Email || u.Id == user.Id))
                {
                    var savedUser = _users.First(u => u.Email == user.Email || u.Id == user.Id);
                    user.Id = savedUser.Id;
                    _users.Remove(savedUser);
                }
                _users.Add(user);
                UpdateSaveFile();
                return Task.Run(() => IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.Run(() => GetIdentityError(ex));
            }
        }


        public Task<IdentityResult> DeleteAsync(BlikiUser user,
            CancellationToken cancellationToken)
        {
            try
            {
                LoadUsers();
                _users.Remove(user);
                UpdateSaveFile();
                return Task.Run(() => IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.Run(() => GetIdentityError(ex));
            }
        }

        public void Dispose()
        {
            // nothing to dispose;
        }

        public Task<BlikiUser> FindByIdAsync(string userId,
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                LoadUsers();
                return _users.FirstOrDefault(u => u.Id == userId);
            }, cancellationToken);
        }

        public Task<BlikiUser> FindByNameAsync(string normalizedUserName,
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                LoadUsers();
                return _users.FirstOrDefault(u => u.NormalizedUserName == normalizedUserName);
            }, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(BlikiUser user,
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.NormalizedUserName;
            }, cancellationToken);
        }

        public Task<string> GetUserIdAsync(BlikiUser user,
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.Id;
            }, cancellationToken);
        }

        public Task<string> GetUserNameAsync(BlikiUser user,
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.UserName;
            }, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(BlikiUser user, string normalizedName,
            CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(BlikiUser user, string userName,
            CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(BlikiUser user,
            CancellationToken cancellationToken)
        {
            try
            {
                LoadUsers();
                var savedUser = _users.FirstOrDefault(u => u.Id == user.Id);
                _users.Remove(savedUser);
                _users.Add(user);
                UpdateSaveFile();
                return Task.Run(() => IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.Run(() => GetIdentityError(ex));
            }
        }

        private void LoadUsers()
        {
            try
            {
                _users.Clear();
                if (File.Exists(_saveFilePath))
                {
                    string json;
                    lock (_fileLock)
                    {
                        json = File.ReadAllText(_saveFilePath);
                    }
                    _users.AddRange(JsonSerializer.Deserialize<BlikiUser[]>(json));
                }
            }
            catch
            {
                //
            }
        }


        private void UpdateSaveFile()
        {
            var json = JsonSerializer.Serialize(_users);
            lock (_fileLock)
            {
                File.WriteAllText(_saveFilePath, json);
            }
        }


        private object _fileLock = new object();

        public IQueryable<BlikiUser> Users {
            get {
                LoadUsers();
                return _users.AsQueryable();
            }
        }

        private IdentityResult GetIdentityError(Exception ex)
        {
            return IdentityResult.Failed(new[]
            {
                new IdentityError
                {
                    Description = ex.Message,
                    Code = ex.GetType().ToString()
                }
            });
        }

        public Task<BlikiUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                LoadUsers();
                return _users.FirstOrDefault(u => u.NormalizedEmail == normalizedEmail);
            }, cancellationToken);
        }

        public Task<string> GetEmailAsync(BlikiUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.Email;
            }, cancellationToken);
        }

        public Task<bool> GetEmailConfirmedAsync(BlikiUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.EmailConfirmed;
            }, cancellationToken);
        }

        public Task<string> GetNormalizedEmailAsync(BlikiUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.NormalizedEmail;
            }, cancellationToken);
        }

        public Task SetEmailAsync(BlikiUser user, string email, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = email.ToUpperInvariant();
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(BlikiUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(BlikiUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task<string> GetPhoneNumberAsync(BlikiUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.PhoneNumber;
            }, cancellationToken);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(BlikiUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.PhoneNumberConfirmed;
            }, cancellationToken);
        }

        public Task SetPhoneNumberAsync(BlikiUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberConfirmedAsync(BlikiUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task<bool> GetTwoFactorEnabledAsync(BlikiUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.TwoFactorEnabled;
            }, cancellationToken);
        }

        public Task SetTwoFactorEnabledAsync(BlikiUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(BlikiUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.PasswordHash;
            }, cancellationToken);
        }

        public Task<bool> HasPasswordAsync(BlikiUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return !string.IsNullOrEmpty(user.PasswordHash);
            }, cancellationToken);
        }

        public Task SetPasswordHashAsync(BlikiUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task AddToRoleAsync(BlikiUser user, string roleName, CancellationToken cancellationToken)
        {
            if (!user.Roles.Contains(roleName))
            {
                var roles = user.Roles.ToList();
                roles.Add(roleName);
                user.Roles = roles.ToArray();
            }
            return Task.CompletedTask;
        }

        public Task<IList<string>> GetRolesAsync(BlikiUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => 
            {
                return user.Roles as IList<string>;
            });
        }

        public Task<IList<BlikiUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                LoadUsers();
                return _users.Where(u => u.Roles.Contains(roleName)).ToArray() as IList<BlikiUser>;
            });
        }

        public Task<bool> IsInRoleAsync(BlikiUser user, string roleName, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.Roles.Contains(roleName);
            });
        }

        public Task RemoveFromRoleAsync(BlikiUser user, string roleName, CancellationToken cancellationToken)
        {
            if (user.Roles.Contains(roleName))
            {
                var roles = user.Roles.ToList();
                roles.Remove(roleName);
                user.Roles = roles.ToArray();
            }
            return Task.CompletedTask;
        }
    }
}
