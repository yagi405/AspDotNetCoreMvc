using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Models.Entities.ViewEntities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = ChatApp.Models.Services.IAuthenticationService;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var identity = _authenticationService.Authenticate(model.UserId, model.Password);
            if (identity == null)
            {
                ModelState.AddModelError("", "ログインに失敗しました。");
                return View(model);
            }

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Chat");
        }
    }
}
