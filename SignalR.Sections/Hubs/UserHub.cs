using Microsoft.AspNetCore.SignalR;

namespace SignalR.Sections.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; } = 0;
        public static int TotalUsers { get; set; } = 0;

        public override async Task OnConnectedAsync()
        {
            TotalUsers++;
            await Clients.All.SendAsync("updateTotalUsers", TotalUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            await Clients.All.SendAsync("updateTotalUsers", TotalUsers);
        }

        public async Task GetTotalViews()
        {
            TotalViews++;
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
        }


    }
}
