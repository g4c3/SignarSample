namespace SignalR.Services
{
    public interface INotificationService
    {

        Task NotifyUser(string userId, string message);
        Task NotifySelectedUsers(List<string> userIds, string message);
        Task NotifyAllUsers(string userId, string message);
        Task NotifyGroup(string groupName, string message);
        Task NotifyGroups(string message, List<string> groupNames);
        Task CreateOrAddToExistingGroup(string groupName);
        Task LeaveGroup(string groupName);
        IEnumerable<string> GetAllGroups();
    }
}
