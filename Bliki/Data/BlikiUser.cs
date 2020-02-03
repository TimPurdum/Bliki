using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics.CodeAnalysis;


namespace Bliki.Data
{
    public class BlikiUser : IdentityUser, IEquatable<BlikiUser>
    {
        public BlikiUser()
        {
        }


        public BlikiUser(string username) : base(username)
        {
        }


        public bool Equals([AllowNull] BlikiUser other)
        {
            return other.Id == Id;
        }


        public string[] Roles { get; set; } = new string[0];
    }
}