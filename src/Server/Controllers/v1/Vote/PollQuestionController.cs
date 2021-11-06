using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteApp.Application.Features.PollQuestions.Commands.Add;
using VoteApp.Shared.Constants.Permission;

namespace VoteApp.Server.Controllers.v1.Vote
{
    public class PollQuestionController : BaseApiController<PollQuestionController>
    {




        //[HttpGet]
        //public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        //{
        //    var products = await _mediator.Send(new GetAllProductsQuery(pageNumber, pageSize, searchString, orderBy));
        //    return Ok(products);
        //}
        [Authorize(Policy = Permissions.Brands.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddPollQuestionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
