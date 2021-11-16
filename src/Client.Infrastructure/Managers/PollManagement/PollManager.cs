using VoteApp.Client.Infrastructure.Extensions;
using VoteApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VoteApp.Application.Features.Polls.Commands.Add;
using VoteApp.Application.Features.Polls.Queries.GetAll;
using VoteApp.Application.Features.Polls.Queries.GetAllPollsByQuestionId;
using System;
using VoteApp.Application.Features.Polls.Queries.GetById;
using VoteApp.Application.Features.Polls.Queries.GetByJoinCode;
using VoteApp.Domain.Entities.Vote;
using VoteApp.Application.Features.Polls.Commands.Stop;

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

        public async Task<IResult<GetPollByIdResponse>> GetByIdAsync(int Id)
        {
            var response = await _httpClient.GetAsync(String.Format(Routes.PollsEndpoints.GetById, Id));
            return await response.ToResult<GetPollByIdResponse>();
        }

        public async Task<IResult<List<GetPollsByQuestionIdResponse>>> GetByQuestionId(int id)
        {
            var staticRoute = Routes.PollsEndpoints.GetByQuestionId;
            var route = String.Format(Routes.PollsEndpoints.GetByQuestionId, id);
            var response = await _httpClient.GetAsync(route);
            return await response.ToResult<List<GetPollsByQuestionIdResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddPollCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(String.Format(Routes.PollsEndpoints.Save, request.PollQuestionId), request);
            return await response.ToResult<int>();
        }



        public async Task<IResult<GetOngoingPollByJoinCodeResponse>> GetOngoingPollByJoinCode(string Joincode)
        {
            var response = await _httpClient.GetAsync(String.Format(Routes.PollsEndpoints.GetByJoinCode, Joincode));
            return await response.ToResult<GetOngoingPollByJoinCodeResponse>();
        }

        public async Task<IResult<int>> VoteOnPollByJoinCode(string Joincode, int GreenVotes, int RedVotes)
        {
            var votecount = new VoteCount()
            {
                GreenVotes = GreenVotes,
                RedVotes = RedVotes
            };
            var response = await _httpClient.PostAsJsonAsync(String.Format(Routes.PollsEndpoints.VoteByJoinCode, Joincode),votecount);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> StopAsync(int id)
        {
            var response = await _httpClient.PostAsJsonAsync(String.Format(Routes.PollsEndpoints.StopPoll,id), new object());
            return await response.ToResult<int>();
        }
    }
}
