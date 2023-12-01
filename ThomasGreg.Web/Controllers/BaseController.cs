using Microsoft.AspNetCore.Mvc;

namespace ThomasGreg.Web.Controllers
{
    public class BaseController : Controller
    {
        public ViewResult Erro404()
        {
            return View("NotFound");
        }
    }
}
