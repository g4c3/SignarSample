using MediatR;
using SignalR.Hub;

namespace SignalR.Handlers.User
{
    public class NotifyUser
    {
        public class Request : IRequest<Response>
        {
            public string? UserId { get; set; }
            public string? Message { get; set; }
        }
        public class Response{}
        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly ComunicationHub _communicationHub;
            public RequestHandler(ComunicationHub communicationHub)
            {
                _communicationHub = communicationHub;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await _communicationHub.SendMessageToUser(request.UserId!, request.Message!);
                 return await Task.FromResult(new Response());
            }
        }
    }
}
