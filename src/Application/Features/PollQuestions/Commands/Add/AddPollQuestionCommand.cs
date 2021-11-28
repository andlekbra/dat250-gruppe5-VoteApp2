using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Shared.Constants.Application;
using VoteApp.Shared.Wrapper;
using VoteApp.Domain.Entities;

namespace VoteApp.Application.Features.PollQuestions.Commands.Add
{
    public class AddPollQuestionCommand : IRequest<Result<int>>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string RedAnswer { get; set; }
        [Required]
        public string GreenAnswer { get; set; }
    }

    internal class AddPollQuestionCommandHandler : IRequestHandler<AddPollQuestionCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddPollQuestionCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddPollQuestionCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddPollQuestionCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddPollQuestionCommand command, System.Threading.CancellationToken cancellationToken)
        {
            var pollQuestion = _mapper.Map<PollQuestion>(command);
            await _unitOfWork.Repository<PollQuestion>().AddAsync(pollQuestion);
            await _unitOfWork.Commit(cancellationToken);
            //await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBrandsCacheKey);

            return await Result<int>.SuccessAsync(pollQuestion.Id, _localizer["Poll question Saved"]);
        }
    }
}
