using Api.Hub.Groups;
using Api.Hub.Users;
using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Reflection;

namespace Api.Hub
{
    public class ComunicationHub : Hub<IClientMethods>, IComunicationHub
    {
        private readonly IHubContext<ComunicationHub, IClientMethods> _hubContext;
        private readonly IHubGroupManager _hubGroupManager;
        private readonly IHubUserManager _hubUserManager;

        public const string Endpoint = "/CommunicationHub";
        public ComunicationHub(IHubContext<ComunicationHub, 
            IClientMethods> hubContext,
            IHubGroupManager hubGroupManager,
            IHubUserManager hubUserManager)
        {
            _hubContext = hubContext;
            _hubGroupManager = hubGroupManager;
            _hubUserManager = hubUserManager;
        }

        public override async Task OnConnectedAsync()
        {
            _hubUserManager.AddMember(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception) =>
            await base.OnDisconnectedAsync(exception);


        public Task SendMessageToUser(string connectionId, string message)
        {
            _hubContext.Clients.User(connectionId).MessageToUser(message);

            return Task.CompletedTask;
        }
        public Task SendMessageToUsers(IEnumerable<string> connectionIds, string message)
        {
            _hubContext.Clients.Users(connectionIds).MessageToUser(message);

            return Task.CompletedTask;
        }
        public Task SendMessageToAllUsers(string message)
        {
            _hubContext.Clients.AllExcept(Context.ConnectionId).MessageToAllUsers(message);

            return Task.CompletedTask;
        }


        public Task SendMessageToGroup(string groupName, string message)
        {
            _hubContext.Clients.Group(groupName).MessageToGroup(message);

            return Task.CompletedTask;
        }
        public Task SendMessageToGroups(IEnumerable<string> groupNames, string message)
        {
            _hubContext.Clients.Groups(groupNames).MessageToGroup(message);

            return Task.CompletedTask;
        }

        public async Task AddToGroup(string groupName)
        {
            var members = _hubUserManager.GetAllMembers();
            if (Context != null)
                await _hubGroupManager.AddToGroupAsync(_hubContext.Groups, Context.ConnectionId, groupName);
            else
                throw new ArgumentException(message: "There are no active connections established with the hub");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            if (Context != null)
                await _hubGroupManager.RemoveFromGroupAsync(_hubContext.Groups, Context.ConnectionId, groupName);
            else
                throw new ArgumentException(message: "There are no active connections established with the hub");
        }


        public IEnumerable<string> GetAllGroups()
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

            var groupNames = groupsDictionary!.Keys.Cast<string>();

            return groupNames;
        }
    }
}
