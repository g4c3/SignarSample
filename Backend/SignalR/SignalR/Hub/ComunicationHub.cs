using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Reflection;

namespace SignalR.Hub
{
    public class ComunicationHub : Hub<IClientMethods>//, IComunicationHub
    {
        private readonly IHubContext<ComunicationHub, IClientMethods> _hubContext;
        private readonly IHubGroupManager _hubGroupManager;
        public const string Endpoint = "/CommunicationHub";
        public ComunicationHub(IHubContext<ComunicationHub, IClientMethods> hubContext, IHubGroupManager hubGroupManager)
        {
            _hubContext = hubContext;
            _hubGroupManager = hubGroupManager;
        }
        public Task SendMessageToUser(string connectionId, string message)
        {
            _hubContext.Clients.User(connectionId).MessageToUser(message);

            return Task.CompletedTask;
        }
        public Task SendMessageToUsers(List<string> connectionIds, string message)
        {
            _hubContext.Clients.Users(connectionIds).MessageToUser(message);

            return Task.CompletedTask;
        }
        public Task SendMessageToAllUsers(string message, string? exception)
        {
            if (!String.IsNullOrWhiteSpace(exception))
                _hubContext.Clients.AllExcept(exception).MessageToAllUsers(message, null);
            else
                _hubContext.Clients.All.MessageToUser(message);
            
            return Task.CompletedTask;
        }
        public Task SendMessageToGroup(string message, string groupName)
        {
            _hubContext.Clients.Group(groupName).MessageToGroup(message);

            return Task.CompletedTask;
        }
        public Task SendMessageToGroups(string message, List<string> groupNames)
        {
            _hubContext.Clients.Groups(groupNames).MessageToGroup(message);

            return Task.CompletedTask;
        }

        public async Task AddToGroup(string groupName)
        {
            await _hubGroupManager.AddToGroupAsync(_hubContext.Groups, Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await _hubGroupManager.RemoveFromGroupAsync(_hubContext.Groups, Context.ConnectionId, groupName);
        }
  
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public List<string> GetAllGroups()
        {
            IGroupManager groupManager = _hubContext.Groups;

            object lifetimeManager = groupManager!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_lifetimeManager")
                .GetValue(groupManager)!;

            object groupsObject = lifetimeManager!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_groups")
                .GetValue(lifetimeManager)!;

            IDictionary? groupsDictionary = groupsObject!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_groups")
                .GetValue(groupsObject) as IDictionary;

            List<string> groupNames = groupsDictionary!.Keys.Cast<string>().ToList();

            return groupNames;
        } 
    }
}
