using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Rabbit;

namespace VoteApp.Application.RabbitJobScheduler
{
    public class HangfireJobScheduler
    {
        public static void ScheduleReccuringJobs()
        {
            RecurringJob.RemoveIfExists(nameof(RabbitJob));
            RecurringJob.AddOrUpdate<RabbitJob>(nameof(RabbitJob),
                job => job.Run(JobCancellationToken.Null),
                Cron.MinuteInterval(5), TimeZoneInfo.Local);

        }
    }
}
