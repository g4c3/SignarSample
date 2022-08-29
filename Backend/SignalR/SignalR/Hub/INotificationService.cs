namespace SignalR.Hub
{
    public interface INotificationService
    {
        Task CreateOrAddToExistingGroup(string groupName);
        Task LeaveGroup(string groupName);
        IEnumerable<string> GetAllGroups();


    }
}
