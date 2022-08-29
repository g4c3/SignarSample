namespace SignalR.Hub
{
    public class NotificationService : INotificationService
    {
        private readonly ComunicationHub _comunicationHub;

        public NotificationService(ComunicationHub comunicationHub)
        {
            _comunicationHub = comunicationHub;
        }

        public async Task CreateOrAddToExistingGroup(string groupName) =>
            await _comunicationHub.AddToGroup(groupName);

        public async Task LeaveGroup(string groupName) =>
            await _comunicationHub.RemoveFromGroup(groupName);

        public IEnumerable<string> GetAllGroups() =>
            _comunicationHub.GetAllGroups();
    }
}
