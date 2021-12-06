using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Application.Interfaces.Services;
using VoteApp.Application.Notifications;
using VoteApp.Domain.Entities;

namespace VoteApp.Application.NotifcationHandlers
{
    class PublishMessageOnPollStopped : INotificationHandler<PollStoppedNotification>
    {

        private readonly IMessageService _messageService;
        private readonly IUnitOfWork<int> _unitOfWork;
        public PublishMessageOnPollStopped(IMessageService messageService, IUnitOfWork<int> unitOfWork)
        {
            _messageService = messageService;
            _unitOfWork = unitOfWork;
        }
        public Task Handle(PollStoppedNotification notification, CancellationToken cancellationToken)
        {
            var message = _unitOfWork.Repository<Poll>().Entities.Where(poll => poll.Id == notification.PollId)
                .Select(poll => new PublishMessageOnPollStoppedMessage()
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

            return _messageService.Send(message);
        }
    }


    public class PublishMessageOnPollStoppedMessage
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
