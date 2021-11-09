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

namespace VoteApp.Application.Features.Polls.Queries.GetAll
{
    public class GetAllPollsQuery : IRequest<PaginatedResult<GetAllPollsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public GetAllPollsQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }

        public GetAllPollsQuery()
        {
        }
    }

    internal class GetAllPollsQueryHandler : IRequestHandler<GetAllPollsQuery, PaginatedResult<GetAllPollsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        public GetAllPollsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPollsResponse>> Handle(GetAllPollsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Poll, GetAllPollsResponse>> expression = e => new GetAllPollsResponse
            {
                Id = e.Id,
                StartTime = e.StartTime,
                StopTime = e.StopTime,
                JoinCode = e.JoinCode,
                PollQuestionId = e.PollQuestionId,
                Question = e.Question.Title,
            };
            var data = await _unitOfWork.Repository<Poll>().Entities
               .Select(expression)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return data;
        }
    }
}