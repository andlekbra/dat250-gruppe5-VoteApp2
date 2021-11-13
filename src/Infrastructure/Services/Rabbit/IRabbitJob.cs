using EasyNetQ;
using Hangfire;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Domain.Entities.Vote;

namespace VoteApp.Application.Interfaces.Rabbit
{
    public interface IRabbitJob
    {
        Task RunAtTimeOf(DateTime now);
    }

    public class RabbitJob : IRabbitJob
    {
        //private readonly ILogger<RabbitJob> _logger;
        //private readonly MyDbContext _dbContext;

        public RabbitJob()//ILogger<RabbitJob> logger)//, MyDbContext dbContext)
        {
            //_logger = logger;
            //_dbContext = dbContext;
        }

        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await RunAtTimeOf(DateTime.Now);

        }

        public async Task RunAtTimeOf(DateTime now)
        {
           //_logger.LogInformation("Job starts...");
            //do some work
            //await _dbContext.SaveChangesAsync
            //
            Poll poll = new Poll();
            TestNetQ(poll);
            //_logger.LogInformation("Job completed");

        }


        public async static Task TestNetQ(Object poll)
        {

            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                var json = JsonConvert.SerializeObject(poll);
                //var body = Encoding.UTF8.GetBytes(json);
                await bus.PubSub.PublishAsync(json).ConfigureAwait(false);
            }
        }
    }
}