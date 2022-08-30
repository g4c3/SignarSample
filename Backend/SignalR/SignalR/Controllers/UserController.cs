using MediatR;
using Microsoft.AspNetCore.Mvc;
using SignalR.Handlers.User;

namespace SignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ISender _sender;
        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("NotifyUser")]
        public async Task<IActionResult> NotifyUser([FromBody] NotifyUser.Request request, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpPost("NotifyAllUsers")]
        public async Task<IActionResult> NotifyAllUsers([FromBody] NotifyAllUsers.Request request, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("NotifySelectedUsers")]
        public async Task<IActionResult> NotifySelectedUsers([FromBody] NotifySelectedUsers.Request request, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(request, cancellationToken);
            
            return Ok(response);
        }
    }
}
