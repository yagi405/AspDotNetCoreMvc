using System;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Extensions
{
    public static class HttpRequestExtensions
    {
        private const string AjaxRequestHeaderItem = "X-Requested-With";
        private const string AjaxRequestHeaderValue = "XMLHttpRequest";

        public static bool IsAjaxRequest(this HttpRequest self)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }
            return self.Headers != null &&
                   self.Headers[AjaxRequestHeaderItem] == AjaxRequestHeaderValue;
        }
    }
}
