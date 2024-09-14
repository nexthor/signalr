using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.Sections.Data;

namespace SignalR.Sections.Hubs
{
    [Authorize]
    public class BasicChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        public BasicChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string sender, string receiver, string message)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName != null && u.UserName.ToLower() == receiver.ToLower());
            if (user == null)
                return;

            await Clients.User(user.Id).SendAsync("receiveMessage", sender, message);
        }

        public async Task SendMessageToAll(string sender, string message)
        {
            await Clients.All.SendAsync("receiveMessage", sender, message);
        }

        public async Task ReceiveMessage(string sender, string receiver, string message)
        {
            await Clients.User(receiver).SendAsync("receiveMessage", sender, message);
        }
    }
}
