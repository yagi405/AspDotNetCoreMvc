using System;
using System.Collections.Generic;
using System.Linq;
using ChatApp.Common;
using ChatApp.Infrastructure.Persistence.Repositories;
using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services.Imp
{
    public class ChatService : IChatService
    {
        private readonly IChatLogRepository _chatLogRepository;
        private readonly IUserRepository _userRepository;

        private const string IconUrlBase = "~/img/app/";

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
                "icon_161720_256.png",
            }
            .Select(x => IconUrlBase + x)
            .ToList();

        public ChatService(IChatLogRepository chatLogRepository, IUserRepository userRepository)
        {
            _chatLogRepository = chatLogRepository;
            _userRepository = userRepository;
        }

        public ChatIndexViewModel GetIndexViewModel(string userId)
        {
            Args.NotEmpty(userId, nameof(userId));

            return new ChatIndexViewModel()
            {
                Details = GetIndexDetailsViewModel(userId),
            };
        }

        public IList<ChatIndexViewModel.Detail> GetIndexDetailsViewModel(string userId)
        {
            Args.NotEmpty(userId, nameof(userId));

            var chatLogs = _chatLogRepository.GetLatest();
            var users = _userRepository.GetAll();

            return chatLogs
                .Select(x => new ChatIndexViewModel.Detail()
                {
                    PostAt = x.PostAt,
                    Message = x.Message,
                    Name = users.SingleOrDefault(u => u.UserId == x.UserId)?.UserName,
                    IconUrl = users.SingleOrDefault(u => u.UserId == x.UserId)?.IconUrl ??
                              _defaultIcons[
                                    Math.Abs(
                                        x.UserId?.ToUpperInvariant().Select(c => (int)c).Sum() ?? 0
                                    ) % _defaultIcons.Count
                              ],
                    IsMine = string.Equals(x.UserId, userId, StringComparison.OrdinalIgnoreCase)
                })
                .ToList();
        }

        public void Post(string message, string userId)
        {
            _chatLogRepository.Add(message, userId);
        }
    }
}
