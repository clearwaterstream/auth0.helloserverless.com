using auth0.helloserverless.com.domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace auth0.helloserverless.com.domain.Requests
{
    public class AddUserRequest : IRequest<UserInfo>
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string tenant { get; set; }
        public string client_id { get; set; }
        public string connection { get; set; }
    }
}
