using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp.Application.Notifications
{
    public class PollStartedNotification : INotification
    {
        public string JoinCode { get; set; }
        public string Question { get; set; }
    }
}
