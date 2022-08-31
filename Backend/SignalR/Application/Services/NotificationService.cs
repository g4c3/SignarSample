using Application.Interfaces;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IComunicationHub _comunicationHub;

        public NotificationService(IComunicationHub comunicationHub) =>
            _comunicationHub = comunicationHub;
        public async Task NotifyUser(string userId, string message) =>
            await _comunicationHub.SendMessageToUser(userId, message);

        public async Task NotifySelectedUsers(IEnumerable<string> userIds, string message) =>
            await _comunicationHub.SendMessageToUsers(userIds, message);

        public async Task NotifyAllUsers(string message) =>
            await _comunicationHub.SendMessageToAllUsers(message);

        public async Task NotifyGroup(string groupName, string message) =>
            await _comunicationHub.SendMessageToGroup(groupName, message);

        public async Task NotifyGroups(IEnumerable<string> groupNames, string message) =>
            await _comunicationHub.SendMessageToGroups(groupNames, message);

        public async Task CreateOrAddToExistingGroup(string connectionId, string groupName) =>
            await _comunicationHub.AddToGroup(connectionId, groupName);

        public async Task LeaveGroup(string connectionId, string groupName) =>
            await _comunicationHub.RemoveFromGroup(connectionId, groupName);

        public IEnumerable<string> GetAllGroups() =>
            _comunicationHub.GetAllGroups();

        public int GetClientsCount() =>
            _comunicationHub.GetClientsCount();

        public IEnumerable<string> GetAllClientIds() =>
            _comunicationHub.GetAllClientIds();
    }
}
