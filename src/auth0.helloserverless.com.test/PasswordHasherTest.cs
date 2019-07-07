using auth0.helloserverless.com.Application.Security;
using Microsoft.AspNetCore.WebUtilities;
using System;
using Xunit;
using Xunit.Abstractions;

namespace auth0.helloserverless.com.test
{
    [Collection("init collection")]
    public class PasswordHasherTest
    {
        readonly InitFixture initFixture;
        readonly ITestOutputHelper output;

        public PasswordHasherTest(InitFixture initFixture, ITestOutputHelper output)
        {
            this.initFixture = initFixture;
            this.output = output;
        }

        [Fact]
        public void HashPlainTest()
        {
            var salt = WebEncoders.Base64UrlDecode("YXzgpiodWACVnhHUuTzJLnYNN3VkL-Zb4AV6GgvbBQE");

            var hasher = new PasswordHasher();

            var result = hasher.Hash("hello", salt);

            Assert.True("e7gD-Cyoo5NyQNuZGJ8pbkjKZ-miqyAr0bV6-J5vHms".Eq(result.HashedValue));
        }
    }
}
