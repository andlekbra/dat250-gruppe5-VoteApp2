using VoteApp.Application.Features.Polls.Queries.GetAll;
using VoteApp.Application.Features.Polls.Queries.GetById;
using VoteApp.Application.Features.Polls.Commands.Add;
using VoteApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoteApp.Application.Features.Polls.Queries.GetAllPollsByQuestionId;
using VoteApp.Application.Features.Polls.Queries.GetByJoinCode;

namespace VoteApp.Client.Infrastructure.Managers.PollManagement
{
    public interface IPollManager : IManager
    {
        Task<IResult<List<GetAllPollsResponse>>> GetAllAsync();

        Task<IResult<GetPollByIdResponse>> GetByIdAsync(int Id);

        Task<IResult<List<GetPollsByQuestionIdResponse>>> GetByQuestionId(int id);

        Task<IResult<GetOngoingPollByJoinCodeResponse>> GetOngoingPollByJoinCode(string Joincode);

        Task<IResult<int>> SaveAsync(AddPollCommand request);

        Task<IResult<int>> DeleteAsync(int id);
        Task<IResult<int>> StopAsync(int id);
        Task<IResult<int>> VoteOnPollByJoinCode(string Joincode, int GreenVotes, int RedVotes);
    }
}
