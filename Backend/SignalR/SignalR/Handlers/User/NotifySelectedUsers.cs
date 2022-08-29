using MediatR;
using SignalR.Hub;

namespace SignalR.Handlers.User
{
    public class NotifySelectedUsers
    {
        public class Request : IRequest<Response>
        {
            public List<string>? UserIds { get; set; }
            public string? Message { get; set; }
        }
        public class Response { }
        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly ComunicationHub _communicationHub;
            public RequestHandler(ComunicationHub communicationHub)
            {
                _communicationHub = communicationHub;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await _communicationHub.SendMessageToUsers(request.UserIds!, request.Message!);
                return await Task.FromResult(new Response());
            }
        }
    }
}
