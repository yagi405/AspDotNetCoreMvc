using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Models.Entities;
using ChatApp.Models.Entities.ViewEntities;
using ChatApp.Models.Services;
using ChatApp.Models.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public LoginController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = TempData[AppConst.TempDataKeyMessage] as string;
            var userId = TempData[AppConst.TempDataKeyUserId] as string;
            return View(new LoginIndexViewModel()
            {
                UserId = userId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var identity = _authService.Authenticate(model.UserId, model.Password, out var user);
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

            return View(new LoginChangePasswordViewModel()
            {
                UserId = userId,
            });
        }

        [HttpPost]
        public IActionResult ChangePassword(LoginChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetById(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            if (_authService.Authenticate(model.UserId, model.CurrentPassword, out _) == null)
            {
                ModelState.AddModelError(
                    nameof(LoginChangePasswordViewModel.CurrentPassword)
                    , "現在のパスワードが違います。");
                return View(model);
            }

            var (salt, hashedPassword) = _authService.GenerateSaltAndHashedPassword(model.NewPassword);
            if (!_userService.ChangePassword(user, salt, hashedPassword))
            {
                ModelState.AddModelError(
                    nameof(LoginChangePasswordViewModel.CurrentPassword)
                    , "パスワードの変更に失敗しました。");
                return View(model);
            }

            TempData[AppConst.TempDataKeyMessage] = "パスワードを変更しました。再度ログインしてください。";
            TempData[AppConst.TempDataKeyUserId] = user.UserId;

            return RedirectToAction(nameof(Index));
        }
    }
}
