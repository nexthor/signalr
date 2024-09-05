using Microsoft.AspNetCore.Mvc;

namespace SignalR.Sections.Controllers
{
    public class Section2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
