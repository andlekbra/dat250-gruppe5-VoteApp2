using VoteApp.Application.Features.PollQuestions.Queries.GetAll;
using VoteApp.Application.Features.PollQuestions.Commands.Add;
using VoteApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace VoteApp.Client.Infrastructure.Managers.PollManagement
{
    public interface IPollQuestionManager : IManager
    {
        Task<IResult<List<GetAllPollQuestionsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddPollQuestionCommand request);

        // Task<IResult<int>> DeleteAsync(int id);
    }
}