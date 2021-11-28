using AutoMapper;
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
        public GetPollsByQuestionIdQuery(int pollQuestionId)
        {
            PollQuestionId = pollQuestionId;
        }
        public int PollQuestionId { get; set; }

        internal class GetAllPollsByQuestionIdQueryHandler : IRequestHandler<GetPollsByQuestionIdQuery, Result<List<GetPollsByQuestionIdResponse>>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;
            private readonly IMapper _mapper;

            public GetAllPollsByQuestionIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<List<GetPollsByQuestionIdResponse>>> Handle(GetPollsByQuestionIdQuery query, CancellationToken cancellationToken)
            {
                if (!_unitOfWork.Repository<Poll>().Entities.Where(poll => poll.Question.Id == query.PollQuestionId).Any())
                {
                    return await Result<List<GetPollsByQuestionIdResponse>>.FailAsync("Poll question does not exist");
                }

                Expression<Func<Poll, GetPollsByQuestionIdResponse>> expression = entity => new GetPollsByQuestionIdResponse
                {
                    Id = entity.Id,
                    StartTime = entity.StartTime,
                    JoinCode = entity.JoinCode,
                    GreenVotes = entity.VoteCount.GreenVotes,
                    RedVotes = entity.VoteCount.RedVotes,
                    StopTime = entity.StopTime,
                };

                var response = await _unitOfWork.Repository<Poll>().Entities.Where(poll => poll.Question.Id == query.PollQuestionId).Select(expression).ToListAsync();

                return await Result<List<GetPollsByQuestionIdResponse>>.SuccessAsync(response);
            }
        }

    }
}