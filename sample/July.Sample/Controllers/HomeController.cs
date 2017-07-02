using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using July.Events;
using July.Sample.Events;

namespace July.Sample.Controllers
{
    public class HomeController : Controller
    {
        private IEventBus EventBus { get; set; }

        public HomeController(IEventBus eventBus)
        {
            EventBus = eventBus;
        }

        public IActionResult Index()
        {
            EventBus.Publish<TestEventData>(new TestEventData());



            return Content("Hello world");
        }
    }
}
