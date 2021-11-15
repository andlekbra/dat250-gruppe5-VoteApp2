using EasyNetQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Application.Models.PollStartNotification;
using VoteApp.Application.Models.PollStopNotification;


namespace VoteApp.Application.Interfaces.Services.Mocks
{
    class PollStopNotificationMock : IPollStopNotificationService
    {
        public async void Notify(PollStopNotificationMessage message)
        {


            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                var json = JsonConvert.SerializeObject(message);
                await bus.PubSub.PublishAsync(json).ConfigureAwait(false);

            }
        }
    }
}
