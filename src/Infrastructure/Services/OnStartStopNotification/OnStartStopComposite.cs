using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Services;
using VoteApp.Application.Models.PollStartNotification;
using VoteApp.Application.Models.PollStopNotification;
using VoteApp.Infrastructure.Services.Dweet;
using VoteApp.Infrastructure.Services.Rabbit;

namespace VoteApp.Infrastructure.Services.OnStartStopNotification
{
    public class OnStartStopComposite : IPollStartNotificationService, IPollStopNotificationService
    {
        private readonly DweetService dweet;
        private readonly RabbitService rabbit;
        public OnStartStopComposite()
        {
            dweet = new DweetService();
            rabbit = new RabbitService();
        }
        public void Notify(PollStartNotificationMessage message)
        {
            dweet.Notify(message);
            rabbit.Notify(message);
        }

        public void Notify(PollStopNotificationMessage message)
        {
            dweet.Notify(message);
            rabbit.Notify(message);
        }
    }
}
