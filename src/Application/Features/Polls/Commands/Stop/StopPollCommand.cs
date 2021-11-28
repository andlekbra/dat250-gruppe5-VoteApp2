﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Domain.Entities;
using VoteApp.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using VoteApp.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using VoteApp.Application.Notifications;

namespace VoteApp.Application.Features.Polls.Commands.Stop
{
    public class StopPollCommand : IRequest<Result<int>>
    {
        public StopPollCommand(int PollId)
        {
            this.PollId = PollId;
        }
        public StopPollCommand()
        {

        }
        public int PollId { get; set; }
    }

    internal class AddPollCommandHandler : IRequestHandler<StopPollCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IPublisher _publisher;

        public AddPollCommandHandler(IUnitOfWork<int> unitOfWork, IPublisher publisher)
        {
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<Result<int>> Handle(StopPollCommand command, CancellationToken cancellationToken)
        {
            var poll = await _unitOfWork.Repository<Poll>().Entities.Include(p => p.VoteCount).Include(p => p.Question).FirstAsync(poll => poll.Id == command.PollId);

            if (poll == null)
            {
                return await Result<int>.FailAsync("No poll with that id");
            }
            if (poll.StopTime != null)
            {
                return await Result<int>.FailAsync("Poll with id" + command.PollId + " is not ongoing");
            }

            poll.StopTime = DateTime.Now;


            await _unitOfWork.Repository<Poll>().UpdateAsync(poll);
            await _unitOfWork.Commit(cancellationToken);


            _ = _publisher.Publish(new PollStoppedNotification()
            {
                GreenAnswer = poll.Question.GreenAnswer,
                RedAnswer = poll.Question.RedAnswer,
                Question = poll.Question.Question,
                QuestionId = poll.Question.Id,
                QuestionTitle = poll.Question.Title,
                StartTime = poll.StartTime,
                StopTime = (DateTime)poll.StopTime,
                VoteCount = poll.VoteCount
            });


            return await Result<int>.SuccessAsync(poll.Id, "Poll stopped");

        }
    }
}
