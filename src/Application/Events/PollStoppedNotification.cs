using MediatR;
using System;
using VoteApp.Domain.Entities;

namespace VoteApp.Application.Notifications
{
    public class PollStoppedNotification : INotification
    {
        public int PollId { get; set; }
    }
}
