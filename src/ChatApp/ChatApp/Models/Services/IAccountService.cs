using System.Security.Claims;
using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services
{
    public interface IAccountService
    {
        string GetUserId(ClaimsPrincipal user);

        AccountIndexViewModel GetNewIndexViewModel(string userId);

        void ChangeUserName(string userId, AccountIndexViewModel model);

        bool Authenticate(string userId, AccountChangePasswordViewModel model);

        void ChangePassword(string userId, AccountChangePasswordViewModel model);

        void ChangeUserIcon(string userId, string filePath);
    }
}
