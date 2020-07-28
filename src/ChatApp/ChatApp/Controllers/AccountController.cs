using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Common;
using ChatApp.Extensions;
using ChatApp.Models.Services;
using ChatApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> ChangeIcon(AccountChangeIconViewModel model)
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

                if (model.Icon.Length > 2097152)
                {
                    ModelState.AddModelError("", "ファイルサイズが大きすぎます。");
                    return View(model);
                }

                string[] permittedExtensions = { ".jpg", ".png", ".gif" };
                var ext = Path.GetExtension(model.Icon.FileName)?.ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("", "サポートされていない拡張子です。");
                    return View(model);
                }

                var iconUrl = await UploadUserIconAsync(model.Icon, $"{userId}{ext}");

                _accountService.ChangeUserIcon(userId, iconUrl);
                TempData[AppConst.TempDataKeyMessage] = "アイコンを変更しました。";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View(model);
            }
        }

        [NonAction]
        private async Task<string> UploadUserIconAsync(IFormFile icon, string fileName)
        {
            Args.NotNull(icon, nameof(icon));
            Args.NotEmpty(fileName, nameof(fileName));

            //Storageを用意していないので、WebルートにUpload
            var folder = Path.Combine(_env.WebRootPath, "img/users");
            var filePath = Path.Combine(folder, fileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await icon.CopyToAsync(fileStream);
            return "~/img/users/" + fileName;
        }
    }
}
