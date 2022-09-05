using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Api.Hub;

public class ComunicationHubCallerContext
{
    private readonly string _connectionId;

    private readonly string? _userIdentifier;

    private readonly ClaimsPrincipal? _user;

    private readonly IDictionary<object, object?> _items;

    private readonly IFeatureCollection _features;

    //private readonly CancellationToken _onnectionAborted;
    public ComunicationHubCallerContext(HubCallerContext callerContext)
    {
        _connectionId = callerContext.ConnectionId;
        _userIdentifier = callerContext.UserIdentifier;
        _user = callerContext.User;
        _items = callerContext.Items;
        _features = callerContext.Features;
        //_onnectionAborted = callerContext.ConnectionAborted;
    }

    public string ConnectionId { get => _connectionId; }
    public string? UserIdentifier { get => _userIdentifier; }
    public ClaimsPrincipal? User { get => _user; }
    public IDictionary<object, object?> Items { get => _items; }
    public IFeatureCollection Features { get => _features; }
    //public CancellationToken ConnectionAborted {  get => _onnectionAborted; }

    //public override void Abort()
    //{
    //    _callerContext.Abort();
    //}
}
