using VoteApp.Client.Infrastructure.Extensions;
using VoteApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VoteApp.Application.Features.PollQuestions.Commands.Add;
using VoteApp.Application.Features.PollQuestions.Queries.GetAll;

namespace VoteApp.Client.Infrastructure.Managers.PollManagement
{
    public class PollQuestionManager : IPollQuestionManager
    {
        private readonly HttpClient _httpClient;

        public PollQuestionManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<IResult<int>> DeleteAsync(int id)
        //{
        //    var response = await _httpClient.DeleteAsync($"{Routes.BrandsEndpoints.Delete}/{id}");
        //    return await response.ToResult<int>();
        //}

        public async Task<IResult<List<GetAllPollQuestionsResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.PollQuestionsEndpoints.GetAll);
            return await response.ToResult<List<GetAllPollQuestionsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddPollQuestionCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.PollQuestionsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}