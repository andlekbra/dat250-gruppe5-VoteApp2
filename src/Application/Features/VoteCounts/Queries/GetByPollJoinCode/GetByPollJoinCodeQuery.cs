using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using VoteApp.Domain.Entities;

namespace VoteApp.Application.Features.VoteCounts.Queries.GetByPollJoinCode
{
	public class GetByPollJoinCodeQuery: IRequest<Result<VoteCount>>
	{

        public string JoinCode { get; set; }

        internal class GetByPollJoinCodeQueryHandler : IRequestHandler<GetByPollJoinCodeQuery, Result<VoteCount>>
            {
                private readonly IUnitOfWork<int> _unitOfWork;
                private readonly IMapper _mapper;

                public GetByPollJoinCodeQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
                {
                    _unitOfWork = unitOfWork;
                    _mapper = mapper;
                }
            /*
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
                }*/

			public async Task<Result<VoteCount>> Handle(GetByPollJoinCodeQuery query, CancellationToken cancellationToken)
			{
                var response = await _unitOfWork.Repository<Poll>().Entities.Include(p => p.VoteCount).Where(p => p.JoinCode == query.JoinCode).Select(p => p.VoteCount).FirstOrDefaultAsync();

                return await Result<VoteCount>.SuccessAsync(response);
			}
		}

        
    }
}
