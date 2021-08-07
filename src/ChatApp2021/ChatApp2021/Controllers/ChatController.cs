using Microsoft.AspNetCore.Mvc;
using ChatApp2021.Models.Services;

namespace ChatApp2021.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        public IActionResult Index()
        {
            return View(_chatService.GetIndexViewModel());
        }
    }
}
