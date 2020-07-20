using System.Security.Claims;
using ChatApp.Common;
using ChatApp.Extensions;
using ChatApp.Infrastructure.Persistence.Repositories;
using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services.Imp
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticator _authenticator;
        private readonly IPasswordManager _passwordManager;

        public AccountService(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager)
        {
            _userRepository = userRepository;
            _authenticator = authenticator;
            _passwordManager = passwordManager;
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            Args.NotNull(user, nameof(user));
            
            return _userRepository.GetById(user.UserId())?.UserId;
        }

        public AccountIndexViewModel GetNewIndexViewModel(string userId)
        {
            Args.NotEmpty(userId, nameof(userId));

            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                return null;
            }

            return new AccountIndexViewModel()
            {
                Name = user.UserName,
            };
        }

        public void ChangeUserName(string userId, AccountIndexViewModel model)
        {
            Args.NotEmpty(userId, nameof(userId));
            Args.NotNull(model, nameof(model));

            _userRepository.ChangeUserName(userId, model.Name);
        }

        public bool Authenticate(string userId, AccountChangePasswordViewModel model)
        {
            Args.NotEmpty(userId, nameof(userId));
            Args.NotNull(model, nameof(model));

            return _authenticator.Authenticate(userId, model.CurrentPassword) != null;
        }

        public void ChangePassword(string userId, AccountChangePasswordViewModel model)
        {
            Args.NotEmpty(userId, nameof(userId));
            Args.NotNull(model, nameof(model));

            var (salt, hashedPassword) = _passwordManager.GenerateSaltAndHashedPassword(model.NewPassword);
            _userRepository.ChangePassword(userId, salt, hashedPassword);
        }
    }
}
