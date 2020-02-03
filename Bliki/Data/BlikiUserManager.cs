using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Bliki.Data
{
    public class BlikiUserManager : UserManager<BlikiUser>
    {
        public BlikiUserManager(IUserStore<BlikiUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<BlikiUser> passwordHasher,
            IEnumerable<IUserValidator<BlikiUser>> userValidators,
            IEnumerable<IPasswordValidator<BlikiUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<BlikiUser>> logger) :
            base(store, optionsAccessor, passwordHasher, userValidators,
                passwordValidators, keyNormalizer, errors, services, logger)
        {
        }


        public override Task<bool> IsInRoleAsync(BlikiUser user, string role)
        {
            return Task.Run(() => user.Roles.Contains(role));
        }


        public bool IsInRole(ClaimsPrincipal principal, string role)
        {
            var user = GetUserAsync(principal).Result;
            return user.Roles.Contains(role);
        }
    }
}