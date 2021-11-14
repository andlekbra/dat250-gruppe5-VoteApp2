using VoteApp.Application.Features.Polls.Queries.GetAll;
using VoteApp.Application.Features.Polls.Commands.Add;
using VoteApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoteApp.Application.Features.Polls.Queries.GetAllPollsByQuestionId;

namespace VoteApp.Client.Infrastructure.Managers.PollManagement
{
    public interface IPollManager : IManager
    {
        Task<IResult<List<GetPollByIdResponse>>> GetAllAsync();

        Task<IResult<List<GetPollsByQuestionIdResponse>>> GetByQuestionId(int id);

        Task<IResult<int>> SaveAsync(AddPollCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}
