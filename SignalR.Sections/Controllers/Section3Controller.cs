using Microsoft.AspNetCore.Mvc;

namespace SignalR.Sections.Controllers
{
    public class Section3Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (SD.DealthyHallowRace.TryGetValue(type, out int value))
                SD.DealthyHallowRace[type] = ++value;

            return Ok();
        }
    }
}
