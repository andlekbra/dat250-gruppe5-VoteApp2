using System.ComponentModel.DataAnnotations;
using AutoMapper;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using VoteApp.Domain.Entities.Vote;
using VoteApp.Application.Models.PollStartNotification;
using VoteApp.Application.Interfaces.Services;

namespace VoteApp.Application.Features.Polls.Commands.Add
{
    public partial class AddPollCommand : IRequest<Result<int>>
    {
        [Required]
        public string JoinCode { get; set; }
        [Required]
        public int PollQuestionId { get; set; }
    }

    internal class AddPollCommandHandler : IRequestHandler<AddPollCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<AddPollCommandHandler> _localizer;
        private readonly IPollStartNotificationService _pollStartNotificationService;   


        public AddPollCommandHandler(IUnitOfWork<int> unitOfWork, IPollStartNotificationService pollStartNotificationService)
        {
            _unitOfWork = unitOfWork;
            _pollStartNotificationService = pollStartNotificationService;
        }

        public async Task<Result<int>> Handle(AddPollCommand command, CancellationToken cancellationToken)
        {

            if (await _unitOfWork.Repository<Poll>().Entities.Where(p => (p.StopTime == null) && (p.JoinCode == command.JoinCode)).AnyAsync())
            {
                return await Result<int>.FailAsync(_localizer["JoinCode already exists."]);
            }

            var question = await _unitOfWork.Repository<PollQuestion>().GetByIdAsync(command.PollQuestionId);

            if (question == null)
            {
                return await Result<int>.FailAsync(_localizer["Question does not exist"]);
            }

            //todo check if pollquestion exists

            var poll = new Poll();

            poll.JoinCode = command.JoinCode;
            poll.Question = question;
            poll.StopTime = null;
            poll.StartTime = DateTime.Now;
            poll.VoteCount = new Domain.Entities.Vote.VoteCount();


            _pollStartNotificationService.Notify(new PollStartNotificationMessage()
            {

                JoinCode = poll.JoinCode,
                Question = poll.Question.Title
            });

            await _unitOfWork.Repository<Poll>().AddAsync(poll);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(poll.Id, "Poll Saved");
            
        }
    }
}
