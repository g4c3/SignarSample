using Application.Interfaces;
using MediatR;

namespace Application.Handlers.User
{
    public class GetAllClientIds
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<string>? AllClients { get; set; }
        }

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly INotificationService _notificationService;
            public RequestHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }
            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var result = new Response { AllClients = _notificationService.GetAllClientIds() };
                
                return Task.FromResult(result);
            }
        }
    }
}
