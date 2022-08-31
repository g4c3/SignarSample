using Application.Handlers.Group;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        private readonly ISender _sender;

        public GroupsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetAllGroups")]
        public async Task<IActionResult> GetGroups(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new GetAllGroups.Request { }, cancellationToken);

            return Ok(response);
        }

        [HttpPost("CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroup.Request request, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("LeaveGroup")]
        public async Task<IActionResult> LeaveGroup([FromBody] LeaveGroup.Request request, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(request, cancellationToken);

            return Ok(response);
        }
    }
}
