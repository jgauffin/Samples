using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Messaging.Services;

namespace Messaging.Areas.Messaging.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHelloService _service;

        public HomeController(IHelloService service)
        {
            _service = service;
        }

        //
        // GET: /Messaging/Home/

        public ActionResult Index()
        {
            var model = _service.GetMessage();
            return View(model);
        }

    }
}
