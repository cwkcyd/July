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

        private ILogger Logger { get; set; }

        public HomeController(IEventBus eventBus, ILogger<HomeController> logger)
        {
            EventBus = eventBus;
            Logger = logger;
        }

        public IActionResult Index()
        {
            EventBus.Publish(new TestEventData());
            Logger.LogInformation("Request");
            return Content("Hello world");
        }
    }
}
