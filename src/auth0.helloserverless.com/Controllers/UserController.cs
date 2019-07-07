using auth0.helloserverless.com.Application.Security;
using auth0.helloserverless.com.domain.Features;
using auth0.helloserverless.com.Util;
using clearwaterstream.IoC;
using clearwaterstream.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Controllers
{
    public class UserController : ControllerBase
    {
        readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> GetInfo(CancellationToken cancellationToken)
        {
            var headers = Request.Headers.AsSingleLine();

            _logger.LogDebug($"headers passed: {headers}");

            var loginInfo = Request.Headers.ParseBasicAuthInfo();

            if (string.IsNullOrEmpty(loginInfo.username) || string.IsNullOrEmpty(loginInfo.password))
                return StatusCode(401);

            var userLookup = ServiceRegistrar.Current.GetInstance<IUserLookup>();

            var userInfo = await userLookup.GetByUsername(loginInfo.username, cancellationToken);

            if(userInfo == null)
                return StatusCode(401);

            if (!userInfo.PasswordInfo.IsValid(loginInfo.password))
                return StatusCode(401);

            userInfo.PasswordInfo = null; // sanitize

            return new JsonResult(userInfo, JsonUtil.LeanSerializerSettings);
        }
    }
}
