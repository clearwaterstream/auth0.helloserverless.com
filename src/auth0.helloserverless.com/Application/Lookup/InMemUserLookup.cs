using auth0.helloserverless.com.domain.Features;
using auth0.helloserverless.com.domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Application.Lookup
{
    public class InMemUserLookup : IUserLookup
    {
        public UserInfo FindByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
