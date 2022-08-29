using SignalR.Hub;

namespace SignalR.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ComunicationHub _comunicationHub;

        public NotificationService(ComunicationHub comunicationHub)
        {
            _comunicationHub = comunicationHub;
        }
        public async Task NotifyUser(string userId, string message) =>
            await _comunicationHub.SendMessageToUser(userId, message);

        public async Task NotifySelectedUsers(List<string> userIds, string message)
        {
            await _comunicationHub.SendMessageToUsers(userIds, message);
        }

        public async Task NotifyAllUsers(string userId, string message)
        {
            await _comunicationHub.SendMessageToAllUsers(message, null);
        }

        public async Task NotifyGroup(string groupName, string message)
        {
            await _comunicationHub.SendMessageToGroup(groupName, message);
        }

        public async Task NotifyGroups(string message, List<string> groupNames)
        {
            await _comunicationHub.SendMessageToGroups(message, groupNames);
        }

        public async Task CreateOrAddToExistingGroup(string groupName) =>
            await _comunicationHub.AddToGroup(groupName);

        public async Task LeaveGroup(string groupName) =>
            await _comunicationHub.RemoveFromGroup(groupName);

        public IEnumerable<string> GetAllGroups() =>
            _comunicationHub.GetAllGroups();
    }
}
