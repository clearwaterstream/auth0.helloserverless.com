using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using auth0.helloserverless.com.domain.Features;
using auth0.helloserverless.com.domain.Model;
using clearwaterstream.AWS.Db;
using clearwaterstream.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Application.Persistence
{
    public class UserDb : IUserLookup, IUserPersistor, IDisposable
    {
        static AmazonDynamoDBClient dbClient;

        static readonly Lazy<AmazonDynamoDBClient> dbClientFactory = new Lazy<AmazonDynamoDBClient>(() =>
        {
            dbClient = DynamoDBClientFactory.CreateClient();

            return dbClient;
        });

        static readonly Lazy<Table> UsersTable = new Lazy<Table>(() =>
        {
            var result = Table.LoadTable(dbClientFactory.Value, "users");

            return result;
        });

        async Task<UserInfo> IUserLookup.GetByUsername(string username, CancellationToken cancellationToken)
        {
            var record = await UsersTable.Value.GetItemAsync(username, cancellationToken);

            if (record == null)
                return null;

            var userInfo = JsonConvert.DeserializeObject<UserInfo>(record.ToJson());

            return userInfo;
        }

        async Task IUserPersistor.Save(UserInfo userInfo, CancellationToken cancellationToken)
        {
            // to-do: move salt to a separate storage. Should not be saved together with the hash value. No point.

            userInfo.LastUpdatedOn = DateTime.UtcNow;

            var json = JsonConvert.SerializeObject(userInfo, JsonUtil.LeanSerializerSettings);

            var record = Document.FromJson(json);

            await UsersTable.Value.PutItemAsync(record, cancellationToken);
        }

        public void Dispose()
        {
            dbClient?.Dispose();

        }
    }
}
