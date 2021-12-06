using System.ComponentModel.DataAnnotations;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using VoteApp.Domain.Entities;
using VoteApp.Application.Notifications;
using Microsoft.EntityFrameworkCore;

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

        private readonly IMediator _publisher;


        public AddPollCommandHandler(IUnitOfWork<int> unitOfWork, IMediator publisher)
        {
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<Result<int>> Handle(AddPollCommand command, CancellationToken cancellationToken)
        {

            if (await _unitOfWork.Repository<Poll>().Entities.Where(p => (p.StopTime == null) && (p.JoinCode == command.JoinCode)).AnyAsync(cancellationToken))
            {
                return await Result<int>.FailAsync("JoinCode already exists.");
            }

            var question = await _unitOfWork.Repository<PollQuestion>().GetByIdAsync(command.PollQuestionId);

            if (question == null)
            {
                return await Result<int>.FailAsync("Question does not exist");
            }

            var poll = new Poll()
            {
                JoinCode = command.JoinCode,
                Question = question,
                StopTime = null,
                StartTime = DateTime.Now,
                VoteCount = new VoteCount()
            };

            var newPoll = await _unitOfWork.Repository<Poll>().AddAsync(poll);
            await _unitOfWork.Commit(cancellationToken);

            await _publisher.Publish(new PollStartedNotification { PollId = newPoll.Id });

            return await Result<int>.SuccessAsync(newPoll.Id, "Poll Saved");

        }
    }
}
