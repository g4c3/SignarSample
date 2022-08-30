using MediatR;
using SignalR.Hub;
using System.ComponentModel.DataAnnotations;

namespace SignalR.Handlers.User
{
    public class NotifySelectedUsers
    {
        public class Request : IRequest<Unit>
        {
            [Required]
            public IEnumerable<string>? UserIds { get; set; }
            [Required]
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
                await _communicationHub.SendMessageToUsers(request.UserIds!, request.Message!);
                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
