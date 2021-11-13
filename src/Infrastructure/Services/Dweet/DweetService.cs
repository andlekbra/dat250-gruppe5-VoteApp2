using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Services;
using VoteApp.Application.Models.PollStartNotification;
using VoteApp.Application.Models.PollStopNotification;

namespace VoteApp.Infrastructure.Services.Dweet
{
    class DweetService : IPollStartNotificationService, IPollStopNotificationService
    {
        public void Notify(PollStartNotificationMessage message)
        {
            throw new NotImplementedException();
        }

        public void Notify(PollStopNotificationMessage message)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync()
        {
            throw new NotImplementedException();
        }
    }
}
