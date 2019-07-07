using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace auth0.helloserverless.com.test
{
    public class InitFixture
    {
        readonly IWebHost Host;

        public InitFixture()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            var builder =  WebHost.CreateDefaultBuilder().UseStartup<Startup>();

            Host = builder.Build();
        }
    }

    [CollectionDefinition("init collection")]
    public class InitCollection : ICollectionFixture<InitFixture>
    {
    }
}
