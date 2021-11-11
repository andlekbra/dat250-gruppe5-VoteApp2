
using VoteApp.Application.Features.Polls.Commands.AddEdit;
using VoteApp.Application.Features.Polls.Commands.Del;
using VoteApp.Application.Features.Polls.Queries.GetAll;
using VoteApp.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VoteApp.Application.Features.Polls.Queries.GetByJoinCode;

namespace VoteApp.Server.Controllers.v1.Vote
{
    public class PollController : BaseApiController<PollController>
    {
        [Authorize(Policy = Permissions.Products.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditPollCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = Permissions.Products.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string joinCode)
        {
            if (string.IsNullOrEmpty(joinCode))
            {
                var poll = await _mediator.Send(new GetAllPollsQuery());
                return Ok(poll);
            }
            else
            {
                var poll = await _mediator.Send(new GetPollByJoinCodeQuery() { JoinCode = joinCode });
                return Ok(poll);
            }

        }

        [Authorize(Policy = Permissions.Products.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePollCommand { Id = id }));
        }
    }
}