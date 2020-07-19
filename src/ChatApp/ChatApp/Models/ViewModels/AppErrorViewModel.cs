using System;

namespace ChatApp.Models.ViewModels
{
    public class AppErrorViewModel
    {
        public int StatusCode { get; set; }

        public Exception Ex { get; set; }
    }
}
