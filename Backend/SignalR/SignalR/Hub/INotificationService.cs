namespace SignalR.Hub
{
    public interface INotificationService
    {
        Task<List<string>> AddOrCreateGroup(string groupName);
    }
}
