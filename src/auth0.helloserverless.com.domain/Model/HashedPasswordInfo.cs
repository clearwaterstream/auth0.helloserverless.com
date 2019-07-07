using System;
using System.Collections.Generic;
using System.Text;

namespace auth0.helloserverless.com.domain.Model
{
    public class HashedPasswordInfo
    {
        public string HashedValue { get; set; }
        public int SaltId { get; set; }
    }
}
