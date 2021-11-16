using VoteApp.Application.Features.Polls.Commands.Del;
using VoteApp.Application.Features.Polls.Queries.GetAll;
using VoteApp.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VoteApp.Application.Features.Polls.Queries.GetByJoinCode;
using VoteApp.Domain.Entities.Vote;
using VoteApp.Application.Features.Polls.Commands.Vote;

namespace VoteApp.Server.Controllers.v1.Vote
{
    public class OngoingPollsController : BaseApiController<OngoingPollsController>
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("{joinCode}")]
        public async Task<IActionResult> GetOngoingPollByJoinCode([FromRoute] string joinCode)
        {
            var poll = await _mediator.Send(new GetOngoingPollByJoinCodeQuery(joinCode));
            return Ok(poll);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{joincode}/votecount")]
        public async Task<IActionResult> VoteOnPoll([FromRoute] string joincode, [FromBody] VoteCount voteCount)
        {
            var command = new VoteOnPollCommand()
            {
                JoinCode = joincode,
                VoteCount = voteCount
            };
            return Ok(await _mediator.Send(command));
        }
    }
}