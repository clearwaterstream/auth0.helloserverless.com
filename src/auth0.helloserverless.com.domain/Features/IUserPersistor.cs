using auth0.helloserverless.com.domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.domain.Features
{
    public interface IUserPersistor
    {
        Task Save(UserInfo userInfo, CancellationToken cancellationToken);
    }
}
