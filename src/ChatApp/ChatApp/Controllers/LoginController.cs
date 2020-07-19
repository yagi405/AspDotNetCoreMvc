using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Common;
using ChatApp.Models.Services;
using ChatApp.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = TempData[AppConst.TempDataKeyUserId] as string;
            return View(_loginService.GetNewIndexViewModel(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var identity = _loginService.Authenticate(model, out var user);
            if (identity == null)
            {
                ModelState.AddModelError("", "ログインに失敗しました。");
                return View(model);
            }

            if (user.PasswordType == PasswordType.PlainText)
            {
                TempData[AppConst.TempDataKeyDefault] = user.UserId;
                return RedirectToAction(nameof(ChangePassword));
            }

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Chat");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var userId = TempData[AppConst.TempDataKeyDefault] as string;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(_loginService.GetNewChangePasswordViewModel(userId));
        }

        [HttpPost]
        public IActionResult ChangePassword(LoginChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var userId = _loginService.GetUserId(model.UserId);
                if (userId == null)
                {
                    return NotFound();
                }

                if (!_loginService.Authenticate(model))
                {
                    ModelState.AddModelError(
                        nameof(LoginChangePasswordViewModel.CurrentPassword)
                        , "現在のパスワードが違います。");
                    return View(model);
                }

                _loginService.ChangePassword(userId, model);

                TempData[AppConst.TempDataKeyMessage] = "パスワードを変更しました。再度ログインしてください。";
                TempData[AppConst.TempDataKeyUserId] = userId;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View(model);
            }
        }
    }
}
