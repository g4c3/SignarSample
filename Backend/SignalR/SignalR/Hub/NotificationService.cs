namespace SignalR.Hub
{
    public class NotificationService : INotificationService
    {
        private readonly ComunicationHub _comunicationHub;

        public NotificationService(ComunicationHub comunicationHub)
        {
            _comunicationHub = comunicationHub;
        }

        public async Task<List<string>> AddOrCreateGroup(string groupName)
        {
            await _comunicationHub.AddToGroup(groupName);
            var groups = _comunicationHub.GetAllGroups();
            return groups;
        }
    }
}
