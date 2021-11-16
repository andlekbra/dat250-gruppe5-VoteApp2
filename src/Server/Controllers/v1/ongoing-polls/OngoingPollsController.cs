
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VoteApp.Application.Features.Polls.Queries.GetByJoinCode;
using VoteApp.Domain.Entities.Vote;
using VoteApp.Application.Features.Polls.Commands.Vote;

using VoteApp.Application.Features.VoteCount.Queries.GetByPollJoinCode;

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

		[AllowAnonymous]
		[HttpGet]
		[Route("{joinCode}/live")]
		public async Task<IActionResult> GetLiveResults([FromRoute] string joinCode)
		{
			VoteCount vc = (await _mediator.Send(new GetByPollJoinCodeQuery() { JoinCode = joinCode })).Data;
			return Ok($"{{\"Red\":{vc.RedVotes}, \"Green\":{vc.GreenVotes}}}");
		}

	}
}