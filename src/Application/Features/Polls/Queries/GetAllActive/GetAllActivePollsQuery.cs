using VoteApp.Application.Extensions;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Shared.Wrapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Domain.Entities.Vote;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace VoteApp.Application.Features.Polls.Queries.GetByJoinCode
{
    public class GetAllActivePollsQuery : IRequest<Result<List<GetAllActivePollsResponse>>>
    {
        public string JoinCode { get; set; }

    }

    internal class GetAllActivePollsQueryHandler : IRequestHandler<GetAllActivePollsQuery, Result<List<GetAllActivePollsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        public GetAllActivePollsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetAllActivePollsResponse>>> Handle(GetAllActivePollsQuery query, CancellationToken cancellationToken)
        {

            //Todo: Check if poll is active

            Expression<Func<Poll, GetActivePollByJoinCodeResponse>> expression = entity => new GetActivePollByJoinCodeResponse
            {
                Id = entity.Id,
                StartTime = entity.StartTime,
                JoinCode = entity.JoinCode,
                QuestionId = entity.Question.Id,
                QuestionTitle = entity.Question.Title,
                Question = entity.Question.Question,
                GreenAnswer = entity.Question.GreenAnswer,
                RedAnswer = entity.Question.RedAnswer
            };

            var response = await _unitOfWork.Repository<Poll>().Entities.Where(entity => entity.StopTime == DateTime.MinValue || entity.StopTime > DateTime.Now).Select(expression).ToListAsync();

            return await Result<List<GetAllActivePollsResponse>>.SuccessAsync();
        }
    }
}