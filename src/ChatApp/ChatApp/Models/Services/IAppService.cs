using System;
using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services
{
    public interface IAppService
    {
        AppErrorViewModel GetNewErrorViewModel(int statusCode, Exception ex);
    }
}
