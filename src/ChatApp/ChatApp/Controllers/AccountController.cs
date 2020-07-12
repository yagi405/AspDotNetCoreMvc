using System;
using ChatApp.Models.Entities.ViewEntities;
using ChatApp.Models.Extensions;
using ChatApp.Models.Services;
using ChatApp.Models.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AccountController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = _userService.GetById(User.UserId());
            if (user == null)
            {
                return NotFound();
            }

            return View(new AccountIndexViewModel()
            {
                Name = user.UserName,
            });
        }

        [HttpPost]
        public IActionResult Index(AccountIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = _userService.GetById(User.UserId());
                if (user == null)
                {
                    return NotFound();
                }

                if (!_userService.ChangeUserName(user, model.Name))
                {
                    ModelState.AddModelError("", "プロフィールの更新に失敗しました。");
                    return View(model);
                }

                TempData[AppConst.TempDataKeyMessage] = "プロフィールを更新しました。";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var user = _userService.GetById(User.UserId());
            if (user == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(AccountChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = _userService.GetById(User.UserId());
                if (user == null)
                {
                    return NotFound();
                }

                if (_authService.Authenticate(user.UserId, model.CurrentPassword, out _) == null)
                {
                    ModelState.AddModelError(
                        nameof(AccountChangePasswordViewModel.CurrentPassword)
                        , "現在のパスワードが違います。");
                    return RedirectToAction(nameof(ChangePassword));
                }

                var (salt, hashedPassword) = _authService.GenerateSaltAndHashedPassword(model.NewPassword);
                if (!_userService.ChangePassword(user, salt, hashedPassword))
                {
                    ModelState.AddModelError("", "パスワードの変更に失敗しました。");
                    return RedirectToAction(nameof(ChangePassword));
                }

                TempData[AppConst.TempDataKeyMessage] = "パスワードを変更しました。";

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
