namespace SignalR.Hub
{
    public interface IComunicationHub
    {
        Task SendMessageToUser(string connectionId, string message);
        Task SendMessageToUsers(List<string> connectionIds, string message);
        Task SendMessageToAllUsers(string message, string? exception);
        Task SendMessageToGroup(string message, string groupName);
        Task SendMessageToGroups(string message, List<string> groupNames);
        Task AddToGroup(string groupName);
        Task RemoveFromGroup(string groupName);
        List<string> GetAllGroups();

    }
}
