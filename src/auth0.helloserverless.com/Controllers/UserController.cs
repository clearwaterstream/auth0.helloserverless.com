using auth0.helloserverless.com.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IActionResult GetInfo()
        {
            var headers = Request.Headers.AsSingleLine();

            _logger.LogDebug($"headers passed: {headers}");

            var loginInfo = Request.Headers.ParseBasicAuthInfo();

            if (string.IsNullOrEmpty(loginInfo.username) || string.IsNullOrEmpty(loginInfo.password))
                return StatusCode(401);

            throw new NotImplementedException();
        }
    }
}
