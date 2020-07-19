using System.Collections.Generic;
using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services
{
    public interface IChatService
    {
        ChatIndexViewModel GetIndexViewModel(string userId);

        IList<ChatIndexViewModel.Detail> GetIndexDetailsViewModel(string userId);

        void Post(string message, string userId);
    }
}
