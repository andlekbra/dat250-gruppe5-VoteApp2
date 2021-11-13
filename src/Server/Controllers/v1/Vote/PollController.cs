
using VoteApp.Application.Features.Polls.Commands.Add;
using VoteApp.Application.Features.Polls.Commands.Del;
using VoteApp.Application.Features.Polls.Queries.GetAll;
using VoteApp.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VoteApp.Server.Controllers.v1.Vote
{
    public class PollController : BaseApiController<PollController>
    {
        [Authorize(Policy = Permissions.Products.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddPollCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = Permissions.Products.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var poll = await _mediator.Send(new GetAllPollsQuery());
            return Ok(poll);
        }

        [Authorize(Policy = Permissions.Products.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePollCommand { Id = id }));
        }
    }
}