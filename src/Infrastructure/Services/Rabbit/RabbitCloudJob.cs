using EasyNetQ;
using Hangfire;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Domain.Entities.Vote;

namespace VoteApp.Infrastructure.Services.Rabbit
{
    public interface IRabbitJob
    {
        Task RunAtTimeOf(DateTime now);
    }

    public class RabbitCloudJob : IRabbitJob
    {

        public RabbitCloudJob()
        {

        }
        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await RunAtTimeOf(DateTime.Now).ConfigureAwait(false);

        }

        public async Task RunAtTimeOf(DateTime now)
        {         
            Poll poll = new Poll();
            await SendData(poll);

        }
        public async static Task SendData(Poll poll)
        {
            using var bus = RabbitHutch.CreateBus("this is where the link should be");
            var Json = JsonConvert.SerializeObject(poll);
            await bus.PubSub.PublishAsync(Json).ConfigureAwait(false);

        }
    }
}
