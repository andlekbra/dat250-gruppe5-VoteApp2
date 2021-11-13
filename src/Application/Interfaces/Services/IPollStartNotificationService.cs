using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Application.Models.PollStartNotification;

namespace VoteApp.Application.Interfaces.Services
{
    public interface IPollStartNotificationService
    {
        public void Notify(PollStartNotificationMessage message);
    }
}
