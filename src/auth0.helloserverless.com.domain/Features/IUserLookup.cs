using auth0.helloserverless.com.domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.domain.Features
{
    public interface IUserLookup
    {
        Task<UserInfo> GetByUsername(string username, CancellationToken cancellationToken);
    }
}
