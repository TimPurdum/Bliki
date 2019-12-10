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
    public class BlikiUserStore : IUserStore<IdentityUser>, IUserEmailStore<IdentityUser>,
        IUserPhoneNumberStore<IdentityUser>, IUserTwoFactorStore<IdentityUser>, 
        IUserPasswordStore<IdentityUser>
    {
        private readonly string _saveFilePath;
        private readonly List<IdentityUser> _users;

        public BlikiUserStore()
        {
            _saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "UserStore.json");
            _users = new List<IdentityUser>();
            LoadUsers();
        }

        public Task<IdentityResult> CreateAsync(IdentityUser user, 
            CancellationToken cancellationToken)
        {
            try
            {
                LoadUsers();
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


        public Task<IdentityResult> DeleteAsync(IdentityUser user, 
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

        public Task<IdentityUser> FindByIdAsync(string userId, 
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                LoadUsers();
                return _users.FirstOrDefault(u => u.Id == userId);
            }, cancellationToken);
        }

        public Task<IdentityUser> FindByNameAsync(string normalizedUserName, 
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                LoadUsers();
                return _users.FirstOrDefault(u => u.NormalizedUserName == normalizedUserName);
            }, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, 
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.NormalizedUserName;
            }, cancellationToken);
        }

        public Task<string> GetUserIdAsync(IdentityUser user, 
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.Id;
            }, cancellationToken);
        }

        public Task<string> GetUserNameAsync(IdentityUser user, 
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.UserName;
            }, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, 
            CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(IdentityUser user, string userName, 
            CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(IdentityUser user, 
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
            _users.Clear();
            if (File.Exists(_saveFilePath))
            {
                string json;
                lock (_fileLock)
                {
                    json = File.ReadAllText(_saveFilePath);
                }
                _users.AddRange(JsonSerializer.Deserialize<IdentityUser[]>(json));
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

        public Task<IdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                LoadUsers();
                return _users.FirstOrDefault(u => u.NormalizedEmail == normalizedEmail);
            }, cancellationToken);
        }

        public Task<string> GetEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.Email;
            }, cancellationToken);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.EmailConfirmed;
            }, cancellationToken);
        }

        public Task<string> GetNormalizedEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.NormalizedEmail;
            }, cancellationToken);
        }

        public Task SetEmailAsync(IdentityUser user, string email, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = email.ToUpperInvariant();
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(IdentityUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task<string> GetPhoneNumberAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.PhoneNumber;
            }, cancellationToken);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.PhoneNumberConfirmed;
            }, cancellationToken);
        }

        public Task SetPhoneNumberAsync(IdentityUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.TwoFactorEnabled;
            }, cancellationToken);
        }

        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return user.PasswordHash;
            }, cancellationToken);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return !string.IsNullOrEmpty(user.PasswordHash);
            }, cancellationToken);
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }
    }
}
