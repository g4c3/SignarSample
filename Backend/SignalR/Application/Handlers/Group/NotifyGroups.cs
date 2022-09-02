using Application.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Handlers.User
{
    public class NotifyGroups
    {
        public class Request : IRequest<Unit>
        {
            [Required]
            public IEnumerable<string>? GroupNames { get; set; }
            [Required]
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
                await _notificationService.NotifyGroups(request.GroupNames!, request.Message!);
                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
