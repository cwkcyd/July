using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Castle.Core;
using July.Events;
using July.Ioc;
using Autofac;

namespace July.Sample.Controllers
{
    public class HomeController : Controller
    {
        private IEventBus Handler { get; set; }

        public HomeController(IEventBus handler)
        {
            Handler = handler;

            var type1 = typeof(IEventHandler<>);
            var type2 = typeof(TestEventHandler);

            bool b = type2.IsAssignableFrom(type1);
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            Handler.Publish<TestEventData>(new TestEventData());

            return Content("Hello world");
        }
    }
}
