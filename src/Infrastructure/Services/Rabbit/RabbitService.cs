using EasyNetQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Services;
using VoteApp.Application.Models.PollStartNotification;
using VoteApp.Application.Models.PollStopNotification;

namespace VoteApp.Infrastructure.Services.Rabbit
{


    public class RabbitService : IPollStartNotificationService, IPollStopNotificationService
    {

        public async void Notify(PollStartNotificationMessage message)
        {
            using (var bus = RabbitHutch.CreateBus("amqps://cfuzuohh:a3WPbqQLs1R9xqyvHVvXBH3nYOwyC61r@hawk.rmq.cloudamqp.com/cfuzuohh"))
            {
                var json = JsonConvert.SerializeObject(message);
                await bus.PubSub.PublishAsync(json).ConfigureAwait(false);
            }

        }

        public async void Notify(PollStopNotificationMessage message)
        {
            using (var bus = RabbitHutch.CreateBus("amqps://cfuzuohh:a3WPbqQLs1R9xqyvHVvXBH3nYOwyC61r@hawk.rmq.cloudamqp.com/cfuzuohh"))
            {
                var json = JsonConvert.SerializeObject(message);
                await bus.PubSub.PublishAsync(json).ConfigureAwait(false);

            }
        }
    }

}
