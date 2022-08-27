using Microsoft.AspNetCore.Mvc;

namespace SignalR.Controllers
{
    public class GroupsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
