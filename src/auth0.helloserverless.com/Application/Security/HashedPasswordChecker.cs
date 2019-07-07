using auth0.helloserverless.com.domain.Features;
using auth0.helloserverless.com.domain.Model;
using clearwaterstream.IoC;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Application.Security
{
    public static class HashedPasswordChecker
    {
        static readonly IPasswordHasher _passwordHasher = ServiceRegistrar.Current.GetInstance<IPasswordHasher>();

        public static bool IsValid(this HashedPasswordInfo pi, string plainTextPassword)
        {
            if (pi == null)
                return false;

            if (string.IsNullOrEmpty(plainTextPassword))
                return false;

            if (string.IsNullOrEmpty(pi.Salt))
                return false;

            if (pi.Salt.Length > 500)
                return false; // suspiciously long

            var salt = WebEncoders.Base64UrlDecode(pi.Salt);

            var passwordHash = _passwordHasher.Hash(plainTextPassword, salt).HashedValue;

            return passwordHash.Eq(pi.HashedValue);
        }
    }
}
