namespace Application.Interfaces
{
    public interface IClientMethods
    {
        Task MessageToUser(string message);
        Task MessageToUsers(List<string> connectionIds, string message);
        Task MessageToAllUsers(string message);
        Task MessageToGroup(string message);
        Task MessageToGroups(string message, List<string> groupNames);
    }
}
