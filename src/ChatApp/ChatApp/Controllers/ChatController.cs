using System;
using ChatApp.Attributes;
using ChatApp.Common;
using ChatApp.Extensions;
using ChatApp.Models.Services;
using ChatApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_chatService.GetIndexViewModel(User.UserId()));
        }

        [HttpPost]
        public IActionResult Index(ChatIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var response = new CommandResponse(false, "入力内容に不備があります。");
                return Json(response);
            }

            try
            {
                if (!string.IsNullOrEmpty(model.Message))
                {
                    _chatService.Post(model.Message, User.UserId());
                }
                return Json(new CommandResponse());
            }
            catch (Exception ex)
            {
                var response = new CommandResponse(false);
                response.AddExtra(ex.ToString());
                return Json(response);
            }
        }

        [HttpPost]
        [AjaxOnly]
        public IActionResult Refresh()
        {
            var details = _chatService.GetIndexDetailsViewModel(User.UserId());
            return PartialView("_ChatLogsPartial", details);
        }
    }
}
