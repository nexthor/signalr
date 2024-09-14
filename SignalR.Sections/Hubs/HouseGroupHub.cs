using Microsoft.AspNetCore.SignalR;

namespace SignalR.Sections.Hubs
{
    public class HouseGroupHub : Hub
    {
        public static List<string> GroupsJoined { get; set; } = [];
        private readonly string Separator = "--";

        public override async Task OnConnectedAsync()
        {
            await GetSubscriptionStatus(false);
        }

        public async Task JoinGroup(string groupName)
        {
            var fullGroupName = $"{Context.ConnectionId}{Separator}{groupName}";
            if (GroupsJoined.Contains(fullGroupName))
                return;

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            GroupsJoined.Add(fullGroupName);
            await GetSubscriptionStatus(true);
            await Clients.Others.SendAsync("newMemberAddedFromHouse", groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            var fullGroupName = $"{Context.ConnectionId}{Separator}{groupName}";
            if (!GroupsJoined.Contains(fullGroupName))
                return;

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            GroupsJoined.Remove(fullGroupName);
            await GetSubscriptionStatus(false);
            await Clients.Others.SendAsync("newMemberRemovedFromHouse", groupName);
        }

        public async Task TriggerHouseNotify(string groupName)
        {
            await Clients.Group(groupName).SendAsync("triggerHouseNotification", groupName);
        }

        private async Task GetSubscriptionStatus(bool hasSubscribed)
        {
            var groupList = GroupsJoined
                                .Where(x => x.Contains(Context.ConnectionId))
                                .Select(x => x.Split(Separator)[1])
                                .ToList();
            await Clients.Caller.SendAsync("subscriptionStatus", string.Join(',', groupList), hasSubscribed);
        }
    }
}
