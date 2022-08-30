using MediatR;
using SignalR.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SignalR.Handlers.User
{
    public class NotifyAllUsers
    {
        public class Request : IRequest<Unit>
        {
            [Required]
            public string? Message { get; set; }
        }

        public class RequstHandler : IRequestHandler<Request, Unit>
        {
            private readonly INotificationService _notificationService;
            public RequstHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                await _notificationService.NotifyAllUsers(request.Message!);

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
