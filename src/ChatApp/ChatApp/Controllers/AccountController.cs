using ChatApp.Models.Entities.ViewEntities;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return View(model);
        }
    }
}
