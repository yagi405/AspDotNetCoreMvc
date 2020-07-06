using CustomAttribute.Models.Extensions;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace CustomAttribute.Models.Attributes
{
    /// <summary>
    /// Ajax呼び出しであるかを検証するアクションセレクターです。
    /// </summary>
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            return routeContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}
