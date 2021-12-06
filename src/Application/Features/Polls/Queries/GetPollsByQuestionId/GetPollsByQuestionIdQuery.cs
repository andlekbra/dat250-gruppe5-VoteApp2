using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Domain.Entities;
using VoteApp.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace VoteApp.Application.Features.Polls.Queries.GetAllPollsByQuestionId
{
    public class GetPollsByQuestionIdQuery : IRequest<Result<List<GetPollsByQuestionIdResponse>>>
    {
        public int PollQuestionId { get; set; }

        internal class GetAllPollsByQuestionIdQueryHandler : IRequestHandler<GetPollsByQuestionIdQuery, Result<List<GetPollsByQuestionIdResponse>>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;

            public GetAllPollsByQuestionIdQueryHandler(IUnitOfWork<int> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<GetPollsByQuestionIdResponse>>> Handle(GetPollsByQuestionIdQuery query, CancellationToken cancellationToken)
            {
                if (!PollQuestionExists(query.PollQuestionId))
                {
                    return await Result<List<GetPollsByQuestionIdResponse>>.FailAsync("Poll question does not exist");
                }

                var polls = await ReadPollsAndMapToResponseObject(query.PollQuestionId);

                return await Result<List<GetPollsByQuestionIdResponse>>.SuccessAsync(polls);
            }

            private bool PollQuestionExists(int PollQuestionId)
            {
                return _unitOfWork.Repository<Poll>().Entities.Where(poll => poll.Question.Id == PollQuestionId).Any();
            }

            private async Task<List<GetPollsByQuestionIdResponse>> ReadPollsAndMapToResponseObject(int PollQuestionId)
            {
                return await _unitOfWork.Repository<Poll>().Entities.Where(poll => poll.Question.Id == PollQuestionId).Select(fromEntityToResponseExpression).ToListAsync();
            }

            private readonly Expression<Func<Poll, GetPollsByQuestionIdResponse>> fromEntityToResponseExpression = entity => new GetPollsByQuestionIdResponse
            {
                Id = entity.Id,
                StartTime = entity.StartTime,
                JoinCode = entity.JoinCode,
                GreenVotes = entity.VoteCount.GreenVotes,
                RedVotes = entity.VoteCount.RedVotes,
                StopTime = entity.StopTime,
            };
        }

    }
}