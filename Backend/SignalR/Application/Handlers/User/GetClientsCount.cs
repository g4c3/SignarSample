using Application.Interfaces;
using MediatR;

namespace Application.Handlers.User;

public class GetClientsCount
{
    public class Request : IRequest<Response> { }

    public class Response
    {
        public int ClientsCount { get; set; }
    }

    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly INotificationService _notificationService;
        public RequestHandler(INotificationService notificationService) => _notificationService = notificationService;
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = new Response { ClientsCount = _notificationService.GetClientsCount() };

            return Task.FromResult(result);
        }
    }
}
