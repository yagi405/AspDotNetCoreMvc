using ChatApp.Models.Entities.ViewEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class AppController : Controller
    {
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public IActionResult Error([Bind(Prefix = "id")] int statusCode = 0)
        {
            switch (statusCode)
            {
                case 404:
                    return RedirectToAction(nameof(PageNotFound));
            }

            var model = new AppErrorViewModel()
            {
                StatusCode = statusCode,
                Ex = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error,
            };

            return View(model);
        }

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
