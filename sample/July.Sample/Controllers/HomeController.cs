using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Castle.Core;

namespace July.Sample.Controllers
{
    public class HomeController : Controller
    {
        private IHandler Handler { get; set; }

        public HomeController(IHandler handler)
        {
            Handler = handler;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            //var handler = new ProxyGenerator().CreateClassProxy<TestHandler>(new TestInterceptor());

            Handler.Test();

            return Content("Hello world");
        }
    }
}
