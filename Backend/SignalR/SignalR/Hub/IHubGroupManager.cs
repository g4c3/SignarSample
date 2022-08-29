﻿using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hub
{
    public interface IHubGroupManager
    {
        Task AddToGroupAsync(IGroupManager groups, string connectionId, string groupName, CancellationToken cancellationToken = default);
        Task RemoveFromGroupAsync(IGroupManager groups, string connectionId, string groupName, CancellationToken cancellationToken = default);
        List<string> GetGroups();
        bool GroupExists(string groupName);
    }
}
