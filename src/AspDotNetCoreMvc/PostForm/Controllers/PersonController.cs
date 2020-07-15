using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostForm.Models;

namespace PostForm.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Create(Person person)
        //{
        //    var method = HttpContext.Request.Method;
        //    return method switch
        //    {
        //        "GET" => View(),
        //        "POST" =>
        //        //実際には作成処理
        //        RedirectToAction(nameof(Index)),
        //        _ => BadRequest()
        //    };
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateWithoutViewModel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateWithoutViewModel(string value)
        {
            return RedirectToAction(nameof(Index));
        }


    }
}
