using System;
using System.Collections.Generic;
using System.Linq;
using ChatApp2021.Infrastructure.Repositories;
using ChatApp2021.Models.ViewModels;

namespace ChatApp2021.Models.Services.Imp
{
    public class ChatService : IChatService
    {
        private readonly IChatLogRepository _chatLogRepository;

        private const string IconUrlBase = "~/images/";

        public ChatService(IChatLogRepository chatLogRepository)
        {
            _chatLogRepository = chatLogRepository;
        }

        private static readonly IReadOnlyList<string> _defaultIcons = new[]
            {
                "icon_106670_256.png",
                "icon_106690_256.png",
                "icon_109030_256.png",
                "icon_118060_256.png",
                "icon_128020_256.png",
                "icon_130880_256.png",
                "icon_138720_256.png",
                "icon_142150_256.png",
                "icon_146830_256.png",
                "icon_161720_256.png"
            }
            .Select(x => IconUrlBase + x)
            .ToList();

        public ChatIndexViewModel GetIndexViewModel()
        {
            return new(GetIndexDetails());
        }

        private IList<ChatIndexViewModel.Detail> GetIndexDetails()
        {
            var chatLogs = _chatLogRepository.GetLatest();

            return chatLogs.Select(x => new ChatIndexViewModel.Detail(
                x.PostAt,
                x.Message,
                "not implemented",
                _defaultIcons.OrderBy(_ => Guid.NewGuid()).FirstOrDefault(),
                false
                ))
                .ToList();
        }
    }
}