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

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _hubUserManager.RemoveMember(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }


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
            _hubContext.Clients.All.MessageToAllUsers(message);

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

        public async Task AddToGroup(string connectionId, string groupName)
        {
            await _hubGroupManager.AddToGroupAsync(_hubContext.Groups, connectionId, groupName);
        }

        public async Task RemoveFromGroup(string connectionId, string groupName)
        {
            await _hubGroupManager.RemoveFromGroupAsync(_hubContext.Groups, connectionId, groupName);
        }


        public IEnumerable<string> GetAllGroups()
        {
            IGroupManager groupManager = _hubContext.Groups;

            DefaultHubLifetimeManager<ComunicationHub>? lifetimeManager = groupManager!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_lifetimeManager")
                .GetValue(groupManager)! as DefaultHubLifetimeManager<ComunicationHub>;

            //groupsObject's type is internal sealed class and cannot be used here
            object groupsObject = lifetimeManager!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_groups")
                .GetValue(lifetimeManager)!;

            IDictionary? groupsDictionary = groupsObject!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_groups")
                .GetValue(groupsObject) as IDictionary;
            
            return groupsDictionary!.Keys.Cast<string>();
        }

        public int GetClientsCount()
        {
            IHubClients<IClientMethods> clients = _hubContext.Clients;

            DefaultHubLifetimeManager<ComunicationHub>? lifetimeManager = clients!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_lifetimeManager")
                .GetValue(clients)! as DefaultHubLifetimeManager<ComunicationHub>;

            HubConnectionStore? connections = lifetimeManager!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_connections")
                .GetValue(lifetimeManager)! as HubConnectionStore;

            return connections!.Count;
        }

        public IEnumerable<string> GetAllClientIds()
        {
            IHubClients<IClientMethods> clients = _hubContext.Clients;

            DefaultHubLifetimeManager<ComunicationHub>? lifetimeManager = clients!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_lifetimeManager")
                .GetValue(clients)! as DefaultHubLifetimeManager<ComunicationHub>;

            //groupsObject's typeis internal sealed class
            object groupsObject = lifetimeManager!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_connections")
                .GetValue(lifetimeManager)!;

            IDictionary? groupsDictionary = groupsObject!.GetType().GetRuntimeFields()
                .Single(fi => fi.Name == "_connections")
                .GetValue(groupsObject) as IDictionary;

            return groupsDictionary!.Keys.Cast<string>();
        }
    }
}
