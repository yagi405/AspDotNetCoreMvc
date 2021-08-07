using System;
using System.Collections.Generic;

namespace ChatApp2021.Models.ViewModels
{
    public record ChatIndexViewModel(
        IList<ChatIndexViewModel.Detail> Details
        )
    {
        public record Detail(
            DateTime PostAt,
            string Message,
            string Name,
            string IconUrl,
            bool IsMine
            );
    }
}