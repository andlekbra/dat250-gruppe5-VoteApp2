using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Domain.Entities.Vote;
using VoteApp.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace VoteApp.Application.Features.Polls.Queries.GetById
{
    public class GetPollByIdQuery : IRequest<Result<GetPollByIdResponse>>
    {
        public int Id { get; set; }

        internal class GetPollByIdQueryHandler : IRequestHandler<GetPollByIdQuery, Result<GetPollByIdResponse>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;
            private readonly IMapper _mapper;

            public GetPollByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<GetPollByIdResponse>> Handle(GetPollByIdQuery query, CancellationToken cancellationToken)
            {
                Expression<Func<Poll, GetPollByIdResponse>> expression = entity => new GetPollByIdResponse
                {
                    Id = entity.Id,
                    StartTime = entity.StartTime,
                    StopTime = entity.StopTime,
                    JoinCode = entity.JoinCode,
                    QuestionId = entity.Question.Id,
                    QuestionTitle = entity.Question.Title,
                    Question = entity.Question.Question,
                    GreenAnswer = entity.Question.GreenAnswer,
                    RedAnswer = entity.Question.RedAnswer,
                    VoteCount = entity.VoteCount
                };
                var response = await _unitOfWork.Repository<Poll>().Entities.Select(expression).FirstAsync(poll => poll.Id == query.Id);

                return await Result<GetPollByIdResponse>.SuccessAsync(response);
            }
        }

    }
}