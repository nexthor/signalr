using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Sections.Hubs;

namespace SignalR.Sections.Controllers
{
    public class Section3Controller : Controller
    {
        private readonly IHubContext<DeathlyHallowsHub> _hubContext;

        public Section3Controller(IHubContext<DeathlyHallowsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (SD.DealthyHallowRace.TryGetValue(type, out int value))
                SD.DealthyHallowRace[type] = ++value;

            await _hubContext.Clients.All.SendAsync("updateDeathlyHallowCount"
                , SD.DealthyHallowRace[SD.Cloak]
                , SD.DealthyHallowRace[SD.Stone]
                , SD.DealthyHallowRace[SD.Wand]);

            return Ok();
        }
    }
}
