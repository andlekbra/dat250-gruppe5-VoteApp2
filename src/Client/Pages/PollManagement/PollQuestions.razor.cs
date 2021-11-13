using VoteApp.Client.Extensions;
using VoteApp.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VoteApp.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using VoteApp.Client.Infrastructure.Managers.PollManagement;
using VoteApp.Application.Features.PollQuestions.Queries.GetAll;
using VoteApp.Application.Features.Polls.Commands.Add;

namespace VoteApp.Client.Pages.PollManagement
{
    public class DummyPollResult
    {
        public int GreenVotes { get; set; }
        public int RedVotes { get; set; }
        public DateTime Started { get; set; }
        public DateTime Stopped { get; set; }

    }
    public partial class PollQuestions
    {
        [Inject] private IPollQuestionManager PollQuestionManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllPollQuestionsResponse> _pollQuestionList = new();
        private GetAllPollQuestionsResponse _pollQuestion = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateBrands;
        private bool _canEditBrands;
        private bool _canDeleteBrands;
        private bool _canExportBrands;
        private bool _canSearchBrands;
        private bool _loaded;

        private GetAllPollQuestionsResponse _selectedQuestion;

        private readonly List<DummyPollResult> _selectedPollResults = new();

        protected override async Task OnInitializedAsync()
        {
            //todo: create permission for pollquestions
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateBrands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Brands.Create)).Succeeded;
            _canEditBrands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Brands.Edit)).Succeeded;
            _canDeleteBrands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Brands.Delete)).Succeeded;
            _canExportBrands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Brands.Export)).Succeeded;
            _canSearchBrands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Brands.Search)).Succeeded;

            await GetPollQuestionsAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }

            _selectedPollResults.Add(new DummyPollResult()
            {
                GreenVotes = 10,
                RedVotes = 11,
                Started = DateTime.Now,
                Stopped = DateTime.Now
            });

            _selectedPollResults.Add(new DummyPollResult()
            {
                GreenVotes = 15,
                RedVotes = 9,
                Started = DateTime.Now,
                Stopped = DateTime.Now
            });

            _selectedPollResults.Add(new DummyPollResult()
            {
                GreenVotes = 23,
                RedVotes = 27,
                Started = DateTime.Now,
                Stopped = DateTime.Now
            });
        }

        private async Task GetPollQuestionsAsync()
        {
            var response = await PollQuestionManager.GetAllAsync();
            if (response.Succeeded)
            {
                _pollQuestionList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        //private async Task Delete(int id)
        //{
        //    string deleteContent = _localizer["Delete Content"];
        //    var parameters = new DialogParameters
        //        {
        //            {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
        //        };
        //    var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
        //    var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
        //    var result = await dialog.Result;
        //    if (!result.Cancelled)
        //    {
        //        var response = await BrandManager.DeleteAsync(id);
        //        if (response.Succeeded)
        //        {
        //            await Reset();
        //            await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
        //            _snackBar.Add(response.Messages[0], Severity.Success);
        //        }
        //        else
        //        {
        //            await Reset();
        //            foreach (var message in response.Messages)
        //            {
        //                _snackBar.Add(message, Severity.Error);
        //            }
        //        }
        //    }
        //}

        private async Task InvokeQuestionModal(int id = 0)
        {
            var parameters = new DialogParameters();

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddPollQuestionModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task InvokePollModal(int id)
        {
            var parameters = new DialogParameters();
            var pollQuestion = _pollQuestionList.FirstOrDefault(c => c.Id == id);
            if (pollQuestion != null)
            {
                parameters.Add(nameof(AddPollModal.AddPollCommand), new AddPollCommand
                {
                    PollQuestionId = pollQuestion.Id
                });
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddPollModal>(_localizer["Start Poll"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _pollQuestion = new GetAllPollQuestionsResponse();
            await GetPollQuestionsAsync();
        }

        private bool Search(GetAllPollQuestionsResponse pollQuestion)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (pollQuestion.Title?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private void OnSelectedQuestion(TableRowClickEventArgs<GetAllPollQuestionsResponse> pollQuestion)
        {
            if (_selectedQuestion == pollQuestion.Item)
            {
                _selectedQuestion = null;
            }
            else
            {
                _selectedQuestion = pollQuestion.Item;
            }
        }
    }
}