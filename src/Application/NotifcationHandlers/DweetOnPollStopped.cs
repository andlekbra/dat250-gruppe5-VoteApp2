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
    public class DweetOnPollStopped : INotificationHandler<PollStoppedNotification>
    {
        private readonly IDweetService _dweetService;
        private readonly IUnitOfWork<int> _unitOfWork;
        public DweetOnPollStopped(IDweetService dweetService, IUnitOfWork<int> unitOfWork)
        {
            _dweetService = dweetService;
            _unitOfWork = unitOfWork;
        }
        public Task Handle(PollStoppedNotification notification, CancellationToken cancellationToken)
        {
            var message = _unitOfWork.Repository<Poll>().Entities.Where(poll => poll.Id == notification.PollId)
                .Select(poll => new DweetOnPollStoppedMessage()
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

            _dweetService.Dweet(JsonSerializer.Serialize(message));

            return Task.CompletedTask;
        }
    }

    public class DweetOnPollStoppedMessage
    {
        public string QuestionTitle { get; set; }
        public string Question { get; set; }
        public int QuestionId { get; set; }
        public string GreenAnswer { get; set; }
        public string RedAnswer { get; set; }
        public VoteCount VoteCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
    }
}
