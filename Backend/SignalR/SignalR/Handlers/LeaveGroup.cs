using MediatR;
using SignalR.Hub;

namespace SignalR.Handlers
{
    public class LeaveGroup
    {
        public class Request : IRequest<Unit>
        {
            public string? GroupName { get; set; }
        }
        public class RequestHandler : IRequestHandler<Request, Unit>
        {
            private readonly INotificationService _notificationService;

            public RequestHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                await _notificationService.LeaveGroup(request.GroupName!);

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
