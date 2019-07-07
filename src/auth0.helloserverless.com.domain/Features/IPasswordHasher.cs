using auth0.helloserverless.com.domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace auth0.helloserverless.com.domain.Features
{
    public interface IPasswordHasher
    {
        HashedPasswordInfo Hash(string plainText);
        HashedPasswordInfo Hash(string plainText, byte[] key);
    }
}
