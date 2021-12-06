using VoteApp.Application.Extensions;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Shared.Wrapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace VoteApp.Application.Features.Polls.Queries.GetByJoinCode
{
    public class GetOngoingPollByJoinCodeQuery : IRequest<Result<GetOngoingPollByJoinCodeResponse>>
    {
        public string JoinCode { get; set; }

        public GetOngoingPollByJoinCodeQuery()
        {

        }

        public GetOngoingPollByJoinCodeQuery(string joinCode)
        {
            JoinCode = joinCode;
        }

    }

    internal class GetActivePollByJoinCodeQueryHandler : IRequestHandler<GetOngoingPollByJoinCodeQuery, Result<GetOngoingPollByJoinCodeResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        public GetActivePollByJoinCodeQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetOngoingPollByJoinCodeResponse>> Handle(GetOngoingPollByJoinCodeQuery query, CancellationToken cancellationToken)
        {

            //Todo: Check if poll is active

            Expression<Func<Poll, GetOngoingPollByJoinCodeResponse>> expression = entity => new GetOngoingPollByJoinCodeResponse
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

            var response = await _unitOfWork.Repository<Poll>().Entities.Select(expression).FirstOrDefaultAsync(poll => poll.JoinCode == query.JoinCode);

            return Result<GetOngoingPollByJoinCodeResponse>.Success(response);
        }
    }
}