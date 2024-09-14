using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalR.Sections.Controllers
{
    public class Section6Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
