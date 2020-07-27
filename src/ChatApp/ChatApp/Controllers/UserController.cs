using System;
using ChatApp.Common;
using ChatApp.Models.Services;
using ChatApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Authorize(Roles = RoleConst.Admin)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_userService.GetIndexViewModel());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (_userService.GetUserId(model.UserId) != null)
                {
                    ModelState.AddModelError("", "そのユーザーIDは既に使用されています。");
                    return View(model);
                }

                _userService.Create(model);
                TempData[AppConst.TempDataKeyMessage] = $"{model.UserId} を作成しました。";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(string userId)
        {
            var model = _userService.GetNewEditViewModel(userId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = _userService.GetUserId(model.UserId);
                if (userId == null)
                {
                    return NotFound();
                }

                _userService.Edit(model);
                
                TempData[AppConst.TempDataKeyMessage] = $"{userId} の情報を更新しました。";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(string userId)
        {
            var model = _userService.GetNewDeleteViewModel(userId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(UserDeleteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = _userService.GetUserId(model.UserId);
                if (userId == null)
                {
                    return NotFound();
                }

                _userService.Delete(model);

                TempData[AppConst.TempDataKeyMessage] = $"{userId} を削除しました。";
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
