using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Controllers
{
    public class UserController : ControllerBase
    {
        static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public IActionResult GetInfo()
        {
            logger.Info("get info called");

            return StatusCode(401);
        }
    }
}
