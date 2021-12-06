using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Domain.Entities;
using VoteApp.Shared.Wrapper;

namespace VoteApp.Application.Features.Polls.Queries.GetAll
{
    public class GetAllPollsQuery : IRequest<Result<List<GetAllPollsResponse>>>
    {

        internal class GetAllPollsQueryHandler : IRequestHandler<GetAllPollsQuery, Result<List<GetAllPollsResponse>>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;
            private readonly IMapper _mapper;

            public GetAllPollsQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<List<GetAllPollsResponse>>> Handle(GetAllPollsQuery request, CancellationToken cancellationToken)
            {
                var pollList = await _unitOfWork.Repository<Poll>().GetAllAsync();
                var mappedPolls = _mapper.Map<List<GetAllPollsResponse>>(pollList);

                return await Result<List<GetAllPollsResponse>>.SuccessAsync(mappedPolls);
            }
        }

    }
}