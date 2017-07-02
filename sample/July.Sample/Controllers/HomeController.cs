using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace July.Sample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.RequestServices.GetService<ILogger<HomeController>>().LogError("hehehe");

            return Content("Hello world");
        }
    }
}
