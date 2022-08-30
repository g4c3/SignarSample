namespace SignalR.Hub
{
    public interface IComunicationHub
    {
        Task SendMessageToUser(string connectionId, string message);
        Task SendMessageToUsers(IEnumerable<string> connectionIds, string message);
        Task SendMessageToAllUsers(string message);
        Task SendMessageToGroup(string groupName, string message);
        Task SendMessageToGroups(IEnumerable<string> groupNames, string message);
        Task AddToGroup(string groupName);
        Task RemoveFromGroup(string groupName);
        IEnumerable<string> GetAllGroups();

    }
}
