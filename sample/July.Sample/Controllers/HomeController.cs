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

            var sub1 = handler.Subscribe<TestEventData, TestEventHandler>();
            var sub2 = handler.Subscribe<TestEventData>(e =>
            {

            });
            var sub3 = handler.Subscribe<TestEventData>(new TestEventHandler());

            handler.Publish(new TestEventData());

            sub1.Dispose();
            sub2.Dispose();
            sub3.Dispose();
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            Handler.Publish<TestEventData>(new TestEventData());

            return Content("Hello world");
        }
    }
}
