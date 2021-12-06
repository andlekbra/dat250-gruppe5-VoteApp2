using EasyNetQ;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Services;
using VoteApp.Application.Notifications;

namespace VoteApp.Infrastructure.Services.Rabbit
{
    internal class RabbitService : IMessageService
    {
        public async static Task SendMessage(Object poll)
        {

            using (var bus = RabbitHutch.CreateBus("amqps://cfuzuohh:ECv7Ll8eRfRzcJK9ITYbCtm97Yo3gXPP@hawk.rmq.cloudamqp.com/cfuzuohh"))
            {
                var json = JsonConvert.SerializeObject(poll);
                await bus.PubSub.PublishAsync(json).ConfigureAwait(false);
            }
        }

        public Task Send(object o)
        {
            return SendMessage(o);
        }
    }
}
