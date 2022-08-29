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

        [HttpPost("{groupName}/CreateGroup")]
        public async Task<IActionResult> GetAllGroups([FromRoute] string groupName)
        {
            var response = await _sender.Send(new CreateGroup.Request { GroupName = groupName });

            return Ok(response);
        }
    }
}
