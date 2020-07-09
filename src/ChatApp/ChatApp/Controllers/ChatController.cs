﻿using ChatApp.Models.Entities.ViewEntities;
using ChatApp.Models.Mappers;
using ChatApp.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatLogService _chatLogService;
        private readonly IChatMapper _chatMapper;

        public ChatController(IChatLogService chatLogService, IChatMapper chatMapper)
        {
            _chatLogService = chatLogService;
            _chatMapper = chatMapper;
        }

        private IActionResult GetIndexActionResult()
        {
            var chatLogs = _chatLogService.GetLatest();
            return View(_chatMapper.FromChatLogToViewModel(chatLogs));
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
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(model.Message))
            {
                _chatLogService.Post(model.Message);
            }

            //Post-Redirect-Get
            return RedirectToAction(nameof(Index));
        }

    }
}
