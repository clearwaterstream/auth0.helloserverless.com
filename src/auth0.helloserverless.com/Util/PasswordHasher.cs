using auth0.helloserverless.com.domain.Features;
using auth0.helloserverless.com.domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Util
{
    public class PasswordHasher : IPasswordHasher
    {
        public HashedPasswordInfo Hash(string plainText)
        {
        }
    }
}
