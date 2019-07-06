using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Content("Home");
        }
    }
}
