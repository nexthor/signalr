using Microsoft.AspNetCore.SignalR;

namespace SignalR.Sections.Hubs
{
    public class NotificationProjectHub : Hub
    {
        private static List<string> _messages = [];
        private static int counter = 0;

        public async Task SendMessage(string message)
        {
            _messages.Add(message);
            counter++;
            await Clients.All.SendAsync("receiveMessage", message, counter);
        }
    }
}
