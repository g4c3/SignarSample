using MediatR;
using SignalR.Services;

namespace SignalR.Handlers.User
{
    public class NotifyAllUser
    {
        public class Request : IRequest<Response>
        {
            public string UserId { get; set; }
            public string Message { get; set; }
        }
        public class Response
        {

        }

        public class RequstHandler : IRequestHandler<Request, Response>
        {
            private readonly INotificationService _notificationService;
            public RequstHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                _notificationService.NotifyAllUsers(request.UserId, request.Message);
                var result = new Response();
                return Task.FromResult(result);
            }
        }
    }
}
