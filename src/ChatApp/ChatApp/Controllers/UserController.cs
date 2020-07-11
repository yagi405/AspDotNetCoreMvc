using System;
using ChatApp.Models.Entities.ViewEntities;
using ChatApp.Models.Mappers;
using ChatApp.Models.Services;
using ChatApp.Models.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Authorize(Roles = RoleConst.Admin)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public UserController(IUserService userService, IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        // GET: UserController
        [HttpGet]
        public IActionResult Index()
        {
            var users = _userService.GetAll();
            return View(_userMapper.FromUserToIndexViewModel(users));
        }

        // GET: UserController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        public IActionResult Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (_userService.GetById(model.UserId) != null)
                {
                    ModelState.AddModelError("", "そのユーザーIDは既に使用されています。");
                    return View(model);
                }

                var user = _userMapper.FromCreateViewModelToUser(model);
                _userService.Create(user);
                TempData[AppConst.TempDataKeyMessage] = $"{user.UserId} を作成しました。";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View(model);
            }
        }

        // GET: UserController/Edit/5
        [HttpGet]
        public IActionResult Edit(string userId)
        {
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(_userMapper.FromUserToEditViewModel(user));
        }

        // POST: UserController/Edit/5
        [HttpPost]
        public IActionResult Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = _userService.GetById(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                if (!_userService.Edit(model.UserId, model.Name, model.IsAdministrator))
                {
                    ModelState.AddModelError("", "ユーザー情報の更新に失敗しました。");
                    return View(model);
                }

                TempData[AppConst.TempDataKeyMessage] = $"{user.UserId} の情報を更新しました。";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View(model);
            }
        }

        // GET: UserController/Delete/5
        [HttpGet]
        public IActionResult Delete(string userId)
        {
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(_userMapper.FromUserToDeleteViewModel(user));
        }

        // POST: UserController/Delete/5
        [HttpPost]
        public IActionResult Delete(UserDeleteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = _userService.GetById(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                if (!_userService.Delete(model.UserId))
                {
                    ModelState.AddModelError("", "ユーザーの削除に失敗しました。");
                    return View(model);
                }

                TempData[AppConst.TempDataKeyMessage] = $"{user.UserId} を削除しました。";
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
