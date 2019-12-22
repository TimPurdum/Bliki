using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Bliki.Data
{
    public class BlikiUser: IdentityUser, IEquatable<BlikiUser>
    {
        public BlikiUser()
        {
        }


        public BlikiUser(string username): base(username)
        {
        }

        public string[] Roles { get; set; } = new string[0];

        public bool Equals([AllowNull] BlikiUser other)
        {
            return other.Id == Id;
        }
    }
}
