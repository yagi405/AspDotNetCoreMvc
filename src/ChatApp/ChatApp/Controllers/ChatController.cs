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

        public IActionResult Index()
        {
            return View();
        }
    }
}
