using System;
using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services.Imp
{
    public class AppService : IAppService
    {
        public AppErrorViewModel GetNewErrorViewModel(int statusCode, Exception ex)
        {
            return new AppErrorViewModel()
            {
                StatusCode = statusCode,
                Ex = ex,
            };
        }
    }
}
