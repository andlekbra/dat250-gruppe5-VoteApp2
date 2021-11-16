using EasyNetQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Application.Models.PollStartNotification;

namespace VoteApp.Application.Interfaces.Services.Mocks
{
    class PollStartNotificationMock : IPollStartNotificationService
    {
        public async void Notify(PollStartNotificationMessage message)
        {
            using( var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                var json = JsonConvert.SerializeObject(message);
                await bus.PubSub.PublishAsync(json).ConfigureAwait(false);
            }
            
        }
    }
}
