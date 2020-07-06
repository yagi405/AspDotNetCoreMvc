using System;
using Microsoft.AspNetCore.Http;

namespace CustomAttribute.Models.Extensions
{
    /// <summary>
    /// <see cref="HttpRequest"/> クラスの拡張メソッドを提供するクラスです。
    /// </summary>
    public static class HttpRequestExtensions
    {
        private const string AjaxRequestHeaderItem = "X-Requested-With";
        private const string AjaxRequestHeaderValue = "XMLHttpRequest";

        /// <summary>
        /// Ajaxによるリクエストかどうかを取得します。
        /// </summary>
        /// <param name="self"><see cref="HttpRequest"/></param>
        /// <returns>Ajaxによるリクエストである場合は true それ以外は false</returns>
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
