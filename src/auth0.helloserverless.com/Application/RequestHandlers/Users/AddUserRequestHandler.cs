using auth0.helloserverless.com.domain.Features;
using auth0.helloserverless.com.domain.Model;
using auth0.helloserverless.com.domain.Requests;
using clearwaterstream.IoC;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Application.RequestHandlers.Users
{
    public class AddUserRequestHandler : IRequestHandler<AddUserRequest, UserInfo>
    {
        static readonly IPasswordHasher _passwordHasher = ServiceRegistrar.Current.GetInstance<IPasswordHasher>();

        public async Task<UserInfo> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return null;

            if (string.IsNullOrEmpty(request.username))
                request.username = request.email; // use email as the username

            var passwordHashInfo = _passwordHasher.Hash(request.password);

            var userInfo = new UserInfo()
            {
                user_id = request.username,
                username = request.username,
                PasswordInfo = passwordHashInfo
            };

            var persistor = ServiceRegistrar.Current.GetInstance<IUserPersistor>();

            await persistor.Save(userInfo, cancellationToken);

            return userInfo;
        }
    }
}
