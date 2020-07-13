using System;
using ChatApp.Models.Attributes;
using ChatApp.Models.Entities.ViewEntities;
using ChatApp.Models.Extensions;
using ChatApp.Models.Mappers;
using ChatApp.Models.Services;
using ChatApp.Models.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatLogService _chatLogService;
        private readonly IChatMapper _chatMapper;

        public ChatController(IChatLogService chatLogService, IChatMapper chatMapper)
        {
            _chatLogService = chatLogService;
            _chatMapper = chatMapper;
        }

        private ChatIndexViewModel GetIndexViewModel()
        {
            var chatLogs = _chatLogService.GetLatest();
            return _chatMapper.FromChatLogToViewModel(chatLogs, User.UserId());
        }

        private IActionResult GetIndexActionResult()
        {
            return View(GetIndexViewModel());
        }

        [HttpGet]
        public IActionResult Index()
        {
            return GetIndexActionResult();
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
                var response = new CommandResponse();
                if (!string.IsNullOrEmpty(model.Message))
                {
                    _chatLogService.Post(model.Message, User.UserId());
                }
                return Json(response);
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
            var details = GetIndexViewModel()?.Details;
            return PartialView("_ChatLogsPartial", details);
        }
    }
}
