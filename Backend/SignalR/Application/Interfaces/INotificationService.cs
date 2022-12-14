namespace Application.Interfaces;

public interface INotificationService
{
    Task NotifyUser(string userId, string message);
    Task NotifySelectedUsers(IEnumerable<string> userIds, string message);
    Task NotifyAllUsers(string message);
    Task NotifyGroup(string groupName, string message);
    Task NotifyGroups(IEnumerable<string> groupNames, string message);
    Task CreateOrAddToExistingGroup(string connectionId, string groupName);
    Task LeaveGroup(string connectionId, string groupName);
    IEnumerable<string> GetAllGroups();
    int GetClientsCount();
    IEnumerable<string> GetAllClientIds();
}
