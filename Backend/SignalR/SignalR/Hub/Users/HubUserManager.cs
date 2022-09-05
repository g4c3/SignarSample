namespace Api.Hub.Users;

public class HubUserManager : IHubUserManager
{
    private readonly List<string> _members;

    public HubUserManager() => _members ??= new List<string>();
    public void AddMember(string id) => _members.Add(id);
    public void RemoveMember(string id) => _members.Remove(id);
    public List<string> GetAllMembers() => _members;
}
