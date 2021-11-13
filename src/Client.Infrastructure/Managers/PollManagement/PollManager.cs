using VoteApp.Client.Infrastructure.Extensions;
using VoteApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VoteApp.Application.Features.Polls.Commands.Add;
using VoteApp.Application.Features.Polls.Queries.GetAll;

namespace VoteApp.Client.Infrastructure.Managers.PollManagement
{
    public class PollManager : IPollManager
    {
        private readonly HttpClient _httpClient;

        public PollManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.PollsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllPollsResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.PollsEndpoints.GetAll);
            return await response.ToResult<List<GetAllPollsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddPollCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.PollsEndpoints.Save, request);
            return await response.ToResult<int>();
        }

    }
}
