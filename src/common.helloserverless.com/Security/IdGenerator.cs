using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace common.helloserverless.com.Util
{
    public static class IdGenerator
    {
        public static string NewId()
        {
            return CryptoRandomGenerator.Instance.GenerateKey();
        }
    }
}
