using System;
using VoteApp.Domain.Entities;

namespace VoteApp.Application.Notifications
{
    public class PollStoppedNotification
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
