using System;
using System.Collections.Generic;
using System.Linq;
using ChatApp.Models.Entities.DbEntities;
using ChatApp.Models.Entities.ViewEntities;
using ChatApp.Models.Services;

namespace ChatApp.Models.Mappers.Imp
{
    public class ChatMapper : IChatMapper
    {
        private readonly IUserService _userService;

        public ChatMapper(IUserService userService)
        {
            _userService = userService;
        }

        private const string IconUrlBase = "~/img/user/";

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

        public ChatIndexViewModel FromChatLogToViewModel(IList<ChatLog> chatLogs, string userId)
        {
            var users = _userService.GetAll();

            var details = chatLogs
                .Select(x => new ChatIndexViewModel.Detail()
                {
                    PostAt = x.PostAt,
                    Message = x.Message,
                    Name = users.SingleOrDefault(u => u.UserId == x.UserId)?.UserName,
                    IconUrl = _defaultIcons[
                        Math.Abs(
                            x.UserId?.ToUpperInvariant().Select(c => (int)c).Sum() ?? 0
                        ) % _defaultIcons.Count],
                    IsMine = string.Equals(x.UserId, userId, StringComparison.OrdinalIgnoreCase)
                })
                .ToList();

            return new ChatIndexViewModel()
            {
                Details = details
            };
        }
    }
}
