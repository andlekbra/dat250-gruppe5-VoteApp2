using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Application.Interfaces.Services;
using VoteApp.Application.Notifications;
using VoteApp.Domain.Entities;

namespace VoteApp.Application.EventHandlers
{
    public class DweetOnPollStarted : INotificationHandler<PollStartedNotification>
    {
        private readonly IDweetService _dweetService;
        private readonly IUnitOfWork<int> _unitOfWork;
        public DweetOnPollStarted(IDweetService dweetService, IUnitOfWork<int> unitOfWork)
        {
            _dweetService = dweetService;
            _unitOfWork = unitOfWork;
        }
        public Task Handle(PollStartedNotification notification, CancellationToken cancellationToken)
        {
            var message = _unitOfWork.Repository<Poll>().Entities.Where(poll => poll.Id == notification.PollId)
                .Select(poll => new DweetOnPollStartedMessage() { JoinCode = poll.JoinCode, QuestionTitle = poll.Question.Title });

            return _dweetService.Dweet(JsonSerializer.Serialize(message));

        }
    }

    public class DweetOnPollStartedMessage
    {
        public string JoinCode { get; set; }
        public string QuestionTitle { get; set; }

    }
}
