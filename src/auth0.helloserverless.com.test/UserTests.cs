﻿using auth0.helloserverless.com.domain.Model;
using auth0.helloserverless.com.domain.Requests;
using clearwaterstream.IoC;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace auth0.helloserverless.com.test
{
    [Collection("init collection")]
    public class UserTests
    {
        [Fact]
        public async Task AddUser()
        {
            var handler = ServiceRegistrar.Current.GetInstance<IRequestHandler<AddUserRequest, UserInfo>>();

            var req = new AddUserRequest()
            {
                Username = "1@2.com",
                Password = "12345"
            };

            var userInfo = await handler.Handle(req, CancellationToken.None);

            Assert.NotNull(userInfo.user_id);
        }
    }
}
