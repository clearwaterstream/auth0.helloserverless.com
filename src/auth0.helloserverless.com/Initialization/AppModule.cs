using auth0.helloserverless.com.Application.Persistence;
using auth0.helloserverless.com.Application.RequestHandlers.Users;
using auth0.helloserverless.com.Application.Security;
using auth0.helloserverless.com.domain.Features;
using Autofac;
using clearwaterstream.Configuration;
using clearwaterstream.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Initialization
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new PasswordHasher()).As<IPasswordHasher>().SingleInstance();

            if (AppEnvironment.IsDevelopment())
            {
                builder.Register(x => new InMemSecretsContainer()).As<ISecretsContainer>().SingleInstance();
            }

            builder.Register(x => new UserDb()).AsImplementedInterfaces().SingleInstance();

            builder.Register(x => new AddUserRequestHandler()).AsImplementedInterfaces().SingleInstance();
        }
    }
}
