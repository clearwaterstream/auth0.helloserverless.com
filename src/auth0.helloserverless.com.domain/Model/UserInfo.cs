using System;
using System.Collections.Generic;
using System.Text;

namespace auth0.helloserverless.com.domain.Model
{
    public class UserInfo
    {
        public string user_id { get; set; }
        public string nickname { get; set; }
        public string username { get; set; }
        public string email { get; set; }

        public HashedPasswordInfo PasswordInfo { get; set; }
        public bool ShouldSerializePasswordInfo() => false;
    }
}
