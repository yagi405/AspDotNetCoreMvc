using System;
using System.IO;
using ChatApp.Common;
using ChatApp.Extensions;
using ChatApp.Models.Services;
using ChatApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _env;

        public AccountController(IAccountService accountService, IWebHostEnvironment env)
        {
            _accountService = accountService;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _accountService.GetNewIndexViewModel(User.UserId());
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
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
                var userId = _accountService.GetUserId(User);
                if (userId == null)
                {
                    return NotFound();
                }

                _accountService.ChangeUserName(userId, model);
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
            var userId = _accountService.GetUserId(User);
            if (userId == null)
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
                var userId = _accountService.GetUserId(User);
                if (userId == null)
                {
                    return NotFound();
                }

                if (!_accountService.Authenticate(userId, model))
                {
                    ModelState.AddModelError(
                        nameof(AccountChangePasswordViewModel.CurrentPassword)
                        , "現在のパスワードが違います。");
                    return View(model);
                }

                _accountService.ChangePassword(userId, model);
                TempData[AppConst.TempDataKeyMessage] = "パスワードを変更しました。";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ChangeIcon()
        {
            var userId = _accountService.GetUserId(User);
            if (userId == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public IActionResult ChangeIcon(AccountChangeIconViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = _accountService.GetUserId(User);
                if (userId == null)
                {
                    return NotFound();
                }

                if (model.Icon == null || model.Icon.Length <= 0)
                {
                    return RedirectToAction(nameof(ChangeIcon));
                }

                var ext = Path.GetExtension(model.Icon.FileName);
                var filePath = Path.Combine(_env.ContentRootPath, "Uploads/users", $"{userId}{ext}");
                using var stream = new FileStream(filePath, FileMode.Create);
                model.Icon.CopyTo(stream);

                _accountService.ChangeUserIcon(userId, filePath);
                TempData[AppConst.TempDataKeyMessage] = "アイコンを変更しました。";

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
