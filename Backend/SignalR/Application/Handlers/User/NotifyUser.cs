using Application.Interfaces;
using MediatR;

namespace Application.Handlers.User
{
    public class NotifyUser
    {
        public class Request : IRequest<Unit>
        {
            public string? UserId { get; set; }
            public string? Message { get; set; }
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
                await _notificationService.NotifyUser(request.UserId!, request.Message!);
                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
