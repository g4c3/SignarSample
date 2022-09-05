namespace Api.Hub.Users;

public interface IHubUserManager
{
    void AddMember(string id);
    void RemoveMember(string id);
    List<string> GetAllMembers();
}
