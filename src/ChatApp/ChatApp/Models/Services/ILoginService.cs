using System.Security.Principal;
using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services
{
    public interface ILoginService
    {
        string GetUserId(string userId);

        IIdentity Authenticate(LoginIndexViewModel model, out ChatAppUser user);

        bool Authenticate(LoginChangePasswordViewModel model);

        LoginIndexViewModel GetNewIndexViewModel(string userId);

        LoginChangePasswordViewModel GetNewChangePasswordViewModel(string userId);

        void ChangePassword(string userId, LoginChangePasswordViewModel model);
    }
}
