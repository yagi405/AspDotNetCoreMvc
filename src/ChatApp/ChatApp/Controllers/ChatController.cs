using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatLogService _chatLogService;

        public ChatController(IChatLogService chatLogService)
        {
            _chatLogService = chatLogService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
