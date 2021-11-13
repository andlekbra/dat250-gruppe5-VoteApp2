using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp.Application.Models.PollStartNotification
{
    public class PollStartNotificationMessage
    {
        public string JoinCode { get; set; }
        public string Question { get; set; }
    }
}
