using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWithModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWithModel.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult List()
        {
            var persons = new List<Person>()
            {
                new Person("山田", 41, new DateTime(1976, 5, 5), false),
                new Person("齋藤", 38, new DateTime(1980, 7, 18), false),
                new Person("田中", 15, new DateTime(2005, 10, 3), false),
                new Person("高橋", 24, new DateTime(1998, 2, 6), true),
            };

            return View(persons);
        }
    }
}
