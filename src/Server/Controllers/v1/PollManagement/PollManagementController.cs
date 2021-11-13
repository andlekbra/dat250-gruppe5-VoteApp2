using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteApp.Application.Features.PollQuestions.Commands.Add;
using VoteApp.Application.Features.PollQuestions.Queries.GetAll;
using VoteApp.Shared.Constants.Permission;


namespace VoteApp.Server.Controllers.v1.Vote
{
    public class PollQuestionsController : BaseApiController<PollQuestionsController>
    {



        [Authorize(Policy = Permissions.Brands.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pollQuestions = await _mediator.Send(new GetAllPollQuestionsQuery());
            return Ok(pollQuestions);
        }

        [Authorize(Policy = Permissions.Brands.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddPollQuestionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


    }
}
