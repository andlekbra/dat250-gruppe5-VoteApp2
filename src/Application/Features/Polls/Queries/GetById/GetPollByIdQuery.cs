using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Domain.Entities.Vote;
using VoteApp.Shared.Wrapper;

namespace VoteApp.Application.Features.Polls.Queries.GetAll
{
    public class GetPollByIdQuery : IRequest<Result<List<GetPollByIdResponse>>>
    {
        public int Id { get; set; }

        internal class GetPollByIdQueryHandler : IRequestHandler<GetPollByIdQuery, Result<List<GetPollByIdResponse>>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;
            private readonly IMapper _mapper;

            public GetPollByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<List<GetPollByIdResponse>>> Handle(GetPollByIdQuery request, CancellationToken cancellationToken)
            {
                var entity = await _unitOfWork.Repository<Poll>().GetByIdAsync(request.Id);
                var response = new GetPollByIdResponse()
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
                    GreenVotes = entity.VoteCount.GreenVotes,
                    RedVotes = entity.VoteCount.RedVotes,

                };





                return await Result<List<GetPollByIdResponse>>.SuccessAsync(mappedPolls);
            }
        }

    }
}