using Application.Interfaces;
using MediatR;

namespace Application.Handlers.Group;

public class GetAllGroups
{
    //how to avoid empty request object?
    public class Request : IRequest<Response> { }

    public class Response
    {
        public IEnumerable<string>? AllGroups { get; set; }
    }

    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly INotificationService _notificationService;
        public RequestHandler(INotificationService notificationService) => _notificationService = notificationService;
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = new Response { AllGroups = _notificationService.GetAllGroups() };

            return Task.FromResult(result);
        }
    }

}
