using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services
{
    public interface IUserService
    {
        string GetUserId(string userId);

        UserIndexViewModel GetIndexViewModel();

        void Create(UserCreateViewModel model);

        UserEditViewModel GetNewEditViewModel(string userId);

        void Edit(UserEditViewModel model);

        UserDeleteViewModel GetNewDeleteViewModel(string userId);

        void Delete(UserDeleteViewModel model);
    }
}
