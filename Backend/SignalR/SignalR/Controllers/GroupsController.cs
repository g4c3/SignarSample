using MediatR;
using Microsoft.AspNetCore.Mvc;
using SignalR.Handlers;

namespace SignalR.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var response = await _sender.Send(new GetAllGroups.Request { });

            return Ok(response);
        }

        [HttpPost("{groupName}/CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromRoute] string groupName)
        {
            var response = await _sender.Send(new CreateGroup.Request { GroupName = groupName });

            return Ok(response);
        }
        [HttpDelete("{groupName}/LeaveGroup")]
        public async Task<IActionResult> LeaveGroup([FromRoute] string groupName)
        {
            var response = await _sender.Send(new LeaveGroup.Request { GroupName = groupName });

            return Ok(response);
        }
    }
}
