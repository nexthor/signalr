using Microsoft.AspNetCore.SignalR;

namespace SignalR.Sections.Hubs
{
    public class HouseGroupHub : Hub
    {
        public static List<string> GroupsJoined { get; set; } = [];

        public async Task JoinGroup(string groupName)
        {
            var fullGroupName = $"{Context.ConnectionId}-{groupName}";
            if (GroupsJoined.Contains(fullGroupName))
                return;

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            GroupsJoined.Add(fullGroupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            var fullGroupName = $"{Context.ConnectionId}-{groupName}";
            if (!GroupsJoined.Contains(fullGroupName))
                return;

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            GroupsJoined.Remove(fullGroupName);
        }
    }
}
