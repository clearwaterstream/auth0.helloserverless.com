using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace common.helloserverless.com.Security
{
    public interface ISecretsContainer
    {
        Task<string> WhisperAsync(string secretId);
    }
}
