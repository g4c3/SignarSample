using MediatR;
using SignalR.Hub;

namespace SignalR.Handlers.User
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
            private readonly ComunicationHub _communicationHub;
            public RequestHandler(ComunicationHub communicationHub)
            {
                _communicationHub = communicationHub;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                await _communicationHub.SendMessageToUser(request.UserId!, request.Message!);
                 return await Task.FromResult(Unit.Value);
            }
        }
    }
}
