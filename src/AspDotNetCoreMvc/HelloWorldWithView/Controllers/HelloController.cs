using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWithView.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Hello World!";
            ViewData["Now"] = DateTime.Now;
            return View();
        }
    }
}
