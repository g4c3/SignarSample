using MediatR;
using SignalR.Hub;

namespace SignalR.Handlers
{
    public class CreateGroup
    {
        public class Request : IRequest<List<string>>
        {
            public string? GroupName { get; set; }
        }

        public class RequestHandler : IRequestHandler<Request, List<string>>
        {
            private readonly INotificationService _notificationService;

            public RequestHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task<List<string>> Handle(Request request, CancellationToken cancellationToken)
            {
                var groups = await _notificationService.AddOrCreateGroup(request.GroupName);
                return groups;
            }
        }
    }
}
