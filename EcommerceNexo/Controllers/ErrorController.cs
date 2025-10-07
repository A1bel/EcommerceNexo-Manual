using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceNexo.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public IActionResult CustomError(string title, string message)
        {
            ViewBag.Title = title ?? "Erro";
            ViewBag.Message = message ?? "Ocorreu um erro inesperado.";
            return View();
        }
    }
}
