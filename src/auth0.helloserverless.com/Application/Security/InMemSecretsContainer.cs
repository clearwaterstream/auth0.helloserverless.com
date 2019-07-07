using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clearwaterstream.Security;

namespace auth0.helloserverless.com.Application.Security
{
    public class InMemSecretsContainer : ISecretsContainer
    {
        readonly Dictionary<string, string> knownValues = new Dictionary<string, string>()
        {
            ["development_password_hash_key"] = "ZuI_YfiYUB-btpiUL6XgNt4MEzVrhBw03kxxDbor7Sg"
        };

        public Task<string> WhisperAsync(string secretId)
        {
            var kvp = knownValues.FirstOrDefault(x => x.Key.Eq(secretId));

            return Task.FromResult(kvp.Value);
        }
    }
}
