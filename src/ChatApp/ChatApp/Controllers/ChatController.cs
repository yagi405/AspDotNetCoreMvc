using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Index()
        {
            return GetIndexActionResult();
        }

    }
}
