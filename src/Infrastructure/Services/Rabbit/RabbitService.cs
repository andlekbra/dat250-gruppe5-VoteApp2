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

    
    internal class RabbitService : IPollStartNotificationService, IPollStopNotificationService
    {
        public async void Notify(PollStopNotificationMessage message)
        {

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            await SendMessage(message).ConfigureAwait(false);
        }
        public async void Notify(PollStartNotificationMessage message)
        {
            await TestNetQ(message);
        }
        public async static Task SendMessage(Object poll)
        {

            using (var bus = RabbitHutch.CreateBus("amqps://cfuzuohh:a3WPbqQLs1R9xqyvHVvXBH3nYOwyC61r@hawk.rmq.cloudamqp.com/cfuzuohh"))
            {
                var json = JsonConvert.SerializeObject(poll);
                //var body = Encoding.UTF8.GetBytes(json);
                await bus.PubSub.PublishAsync(json).ConfigureAwait(false);
            }
        }
    }
   
}
