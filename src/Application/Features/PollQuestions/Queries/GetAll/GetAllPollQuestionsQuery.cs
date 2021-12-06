using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Domain.Entities;
using VoteApp.Shared.Wrapper;

namespace VoteApp.Application.Features.PollQuestions.Queries.GetAll
{
    public class GetAllPollQuestionsQuery : IRequest<Result<List<GetAllPollQuestionsResponse>>>
    {

        internal class GetAllPollQuestionsQueryHandler : IRequestHandler<GetAllPollQuestionsQuery, Result<List<GetAllPollQuestionsResponse>>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;
            private readonly IMapper _mapper;

            public GetAllPollQuestionsQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<List<GetAllPollQuestionsResponse>>> Handle(GetAllPollQuestionsQuery request, CancellationToken cancellationToken)
            {
                var pollQuestionList = await _unitOfWork.Repository<PollQuestion>().GetAllAsync();
                var mappedPollQuestions = _mapper.Map<List<GetAllPollQuestionsResponse>>(pollQuestionList);

                return await Result<List<GetAllPollQuestionsResponse>>.SuccessAsync(mappedPollQuestions);
            }
        }

    }
}
