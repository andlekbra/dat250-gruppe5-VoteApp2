using MediatR;

namespace VoteApp.Application.Notifications
{
    public class PollStartedNotification : INotification
    {
        public int PollId { get; set; }
    }
}
