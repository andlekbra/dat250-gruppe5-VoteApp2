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

namespace VoteApp.Application.Features.Polls.Queries.GetByJoinCode
{
    public class GetActivePollByJoinCodeQuery : IRequest<Result<GetActivePollByJoinCodeResponse>>
    {
        public string JoinCode { get; set; }

        public GetActivePollByJoinCodeQuery()
        {

        }

        public GetActivePollByJoinCodeQuery(string joinCode)
        {
            JoinCode = joinCode;
        }

    }

    internal class GetActivePollByJoinCodeQueryHandler : IRequestHandler<GetActivePollByJoinCodeQuery, Result<GetActivePollByJoinCodeResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        public GetActivePollByJoinCodeQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetActivePollByJoinCodeResponse>> Handle(GetActivePollByJoinCodeQuery query, CancellationToken cancellationToken)
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

            var response = await _unitOfWork.Repository<Poll>().Entities.Select(expression).FirstOrDefaultAsync(poll => poll.JoinCode == query.JoinCode);

            return Result<GetActivePollByJoinCodeResponse>.Success(response);
        }
    }
}