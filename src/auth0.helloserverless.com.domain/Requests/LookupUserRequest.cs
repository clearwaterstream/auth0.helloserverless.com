using auth0.helloserverless.com.domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace auth0.helloserverless.com.domain.Requests
{
    public class LookupUserRequest : IRequest<UserInfo>
    {
        public string Username { get; set; }
    }
}
