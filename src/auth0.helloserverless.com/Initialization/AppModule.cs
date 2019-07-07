using auth0.helloserverless.com.Application.RequestHandlers.Users;
using auth0.helloserverless.com.Application.Security;
using Autofac;
using common.helloserverless.com.Configuration;
using common.helloserverless.com.Security;
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
            if(AppEnvironment.IsDevelopment())
            {
                builder.Register(x => new InMemSecretsContainer()).As<ISecretsContainer>().SingleInstance();
            }

            builder.Register(x => new AddUserRequestHandler()).AsImplementedInterfaces().SingleInstance();
        }
    }
}
