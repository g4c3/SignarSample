using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Api.Hub.Groups
{
    public class HubGroupManager : IHubGroupManager
    {
        private readonly ConcurrentDictionary<string, HashSet<string>> _groups;
        private readonly object _groupsLock = new();

        public HubGroupManager()
        {
            _groups = _groups is null ? new()! : _groups;
        }

        public async Task AddToGroupAsync(IGroupManager groups, string connectionId, string groupName, CancellationToken cancellationToken = default)
        {
            await groups.AddToGroupAsync(connectionId, groupName, cancellationToken);

            lock (_groupsLock)
                if (GroupExists(groupName))
                    _groups[groupName].Add(connectionId);
                else
                    _groups.TryAdd(groupName, new HashSet<string>() { connectionId });
        }

        public async Task RemoveFromGroupAsync(IGroupManager groups, string connectionId, string groupName, CancellationToken cancellationToken = default)
        {
            if (GroupExists(groupName))
            {
                await groups.RemoveFromGroupAsync(connectionId, groupName, cancellationToken);
                lock (_groupsLock)
                {
                    if (_groups[groupName].Contains(connectionId))
                        _groups[groupName].RemoveWhere(cv => cv == connectionId);

                    if (!_groups[groupName].Any())
                    {
                        _groups.TryRemove(groupName, out HashSet<string>? val);
                    }
                }
            }
        }

        public List<string> GetGroups() =>
            _groups.Keys.ToList();

        public bool GroupExists(string groupName)
            => _groups.ContainsKey(groupName);

    }
}
