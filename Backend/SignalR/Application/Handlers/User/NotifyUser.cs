using Application.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Handlers.User;

public class NotifyUser
{
    public class Request : IRequest<Unit>
    {
        [Required]
        public string? SenderId { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public string? Message { get; set; }
    }

    public class RequestHandler : IRequestHandler<Request, Unit>
    {
        private readonly INotificationService _notificationService;
        public RequestHandler(INotificationService notificationService) => _notificationService = notificationService;

        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            await _notificationService.NotifyUser(request.UserId!, request.Message!);
            return await Task.FromResult(Unit.Value);
        }
    }
}
