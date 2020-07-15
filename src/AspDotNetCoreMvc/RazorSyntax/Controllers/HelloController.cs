using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RazorSyntax.Models;

namespace RazorSyntax.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello World!");
        }

        public IActionResult Details()
        {
            var person = new Person("齋藤", 38, new DateTime(1980, 7, 18), false);
            return View(person);
        }
    }
}
