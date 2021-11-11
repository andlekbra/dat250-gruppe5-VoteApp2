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
    public class GetPollByJoinCodeQuery : IRequest<Result<GetPollByJoinCodeResponse>>
    {
        public string JoinCode { get; set; }

    }

    internal class GetPollByJoinCodeQueryHandler : IRequestHandler<GetPollByJoinCodeQuery, Result<GetPollByJoinCodeResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        public GetPollByJoinCodeQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPollByJoinCodeResponse>> Handle(GetPollByJoinCodeQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Poll, GetPollByJoinCodeResponse>> expression = entity => new GetPollByJoinCodeResponse
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

            return Result<GetPollByJoinCodeResponse>.Success(response);
        }
    }
}