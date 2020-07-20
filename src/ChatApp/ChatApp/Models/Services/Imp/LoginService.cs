using System.Security.Principal;
using ChatApp.Common;
using ChatApp.Infrastructure.Persistence.Repositories;
using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services.Imp
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticator _authenticator;
        private readonly IPasswordManager _passwordManager;

        public LoginService(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager)
        {
            _userRepository = userRepository;
            _authenticator = authenticator;
            _passwordManager = passwordManager;
        }

        public string GetUserId(string userId)
        {
            Args.NotEmpty(userId, nameof(userId));
            return _userRepository.GetById(userId)?.UserId;
        }

        public IIdentity Authenticate(LoginIndexViewModel model, out ChatAppUser user)
        {
            return _authenticator.Authenticate(model.UserId, model.Password, out user);
        }

        public bool Authenticate(LoginChangePasswordViewModel model)
        {
            return _authenticator.Authenticate(model.UserId, model.CurrentPassword) != null;
        }

        public LoginIndexViewModel GetNewIndexViewModel(string userId)
        {
            return new LoginIndexViewModel()
            {
                UserId = userId,
            };
        }

        public LoginChangePasswordViewModel GetNewChangePasswordViewModel(string userId)
        {
            return new LoginChangePasswordViewModel()
            {
                UserId = userId,
            };
        }

        public void ChangePassword(string userId, LoginChangePasswordViewModel model)
        {
            Args.NotEmpty(userId, nameof(userId));
            Args.NotNull(model, nameof(model));

            var (salt, hashedPassword) = _passwordManager.GenerateSaltAndHashedPassword(model.NewPassword);
            _userRepository.ChangePassword(userId, salt, hashedPassword);
        }
    }
}
