namespace SignalR.Hub
{
    public interface IComunicationHub
    {
        Task SendMessageToUser(string connectionId, string message);
        Task SendMessageToUsers(List<string> connectionIds, string message);
        Task SendMessageToAllUsers(string message, string? exception);
        Task SendMessageToGroup(string groupName, string message);
        Task SendMessageToGroups(List<string> groupNames, string message);
        Task AddToGroup(string groupName);
        Task RemoveFromGroup(string groupName);
        List<string> GetAllGroups();

    }
}
