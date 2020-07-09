using System;
using System.Collections.Generic;
using System.Linq;
using ChatApp.Models.Entities.DbEntities;
using ChatApp.Models.Entities.ViewEntities;

namespace ChatApp.Models.Mappers.Imp
{
    public class ChatMapper : IChatMapper
    {
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

        public ChatIndexViewModel FromChatLogToViewModel(IList<ChatLog> chatLogs, string userName)
        {
            var details = chatLogs
                .Select(x => new ChatIndexViewModel.Detail()
                {
                    PostAt = x.PostAt,
                    Message = x.Message,
                    Name = x.Name,
                    IconUrl = _defaultIcons[
                        Math.Abs(
                            x.Name?.ToUpperInvariant().Select(c => (int)c).Sum() ?? 0
                        ) % _defaultIcons.Count],
                    IsMine = string.Equals(x.Name, userName, StringComparison.OrdinalIgnoreCase)
                })
                .ToList();

            return new ChatIndexViewModel()
            {
                Details = details
            };
        }
    }
}
