using auth0.helloserverless.com.domain.Features;
using auth0.helloserverless.com.domain.Model;
using common.helloserverless.com.Configuration;
using common.helloserverless.com.IoC;
using common.helloserverless.com.Security;
using common.helloserverless.com.Util;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Application.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        Lazy<byte[]> keyStaticPortion = new Lazy<byte[]>(() =>
        {
            var env = AppEnvironment.Name;

            var secretId = $"{env}_password_hash_key";

            var secretsContainer = ServiceRegistrar.Current.GetInstance<ISecretsContainer>();

            var base64Encoded = secretsContainer.WhisperAsync(secretId).GetAwaiter().GetResult();

            var result = WebEncoders.Base64UrlDecode(base64Encoded);

            return result;
        });

        public HashedPasswordInfo Hash(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            var key = CryptoRandomGenerator.Instance.GenerateBytes(32);

            var result = Hash(plainText, key);

            return result;
        }

        public HashedPasswordInfo Hash(string plainText, byte[] salt)
        {
            var staticPortion = keyStaticPortion.Value;

            var key = new byte[64];

            staticPortion.CopyTo(key, 0);
            salt.CopyTo(key, 32);

            using (var hasher = new HMACSHA256(salt))
            {
                using (var ms = new MemoryStream())
                {
                    using (var sw = new StreamWriter(ms))
                    {
                        sw.Write(plainText);

                        sw.Flush();
                        ms.Position = 0;

                        var hashedBytes = hasher.ComputeHash(ms);

                        var hashedPassword = WebEncoders.Base64UrlEncode(hashedBytes);

                        return new HashedPasswordInfo()
                        {
                            Salt = salt,
                            HashedValue = hashedPassword
                        };
                    }
                }
            }
        }
    }
}
