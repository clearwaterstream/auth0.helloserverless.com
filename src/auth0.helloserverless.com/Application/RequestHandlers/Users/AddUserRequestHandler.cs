using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using auth0.helloserverless.com.domain.Features;
using auth0.helloserverless.com.domain.Model;
using auth0.helloserverless.com.domain.Requests;
using common.helloserverless.com.AWS.Db;
using common.helloserverless.com.IoC;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Application.RequestHandlers.Users
{
    public class AddUserRequestHandler : IRequestHandler<AddUserRequest, UserInfo>, IDisposable
    {
        static readonly IPasswordHasher _passwordHasher = ServiceRegistrar.Current.GetInstance<IPasswordHasher>();

        readonly Lazy<AmazonDynamoDBClient> dbClientFactory = new Lazy<AmazonDynamoDBClient>(() =>
        {
            var client = DynamoDBClientFactory.CreateClient();

            usersTable = Table.LoadTable(client, "users");

            return client;
        });

        AmazonDynamoDBClient dBClient => dbClientFactory.Value;

        static Table usersTable;

        public async Task<UserInfo> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return null;

            var passwordHashInfo = _passwordHasher.Hash(request.Username);

            var record = new Document(new Dictionary<string, DynamoDBEntry>()
            {
                ["username"] = request.Username,
                ["password_salt"] = passwordHashInfo.Salt,
                ["password_hash"] = passwordHashInfo.HashedValue
            });

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            dBClient?.Dispose();
        }
    }
}
