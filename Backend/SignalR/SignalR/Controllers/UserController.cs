using Microsoft.AspNetCore.Mvc;

namespace SignalR.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
