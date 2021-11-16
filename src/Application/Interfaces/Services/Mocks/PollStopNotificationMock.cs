using EasyNetQ;
using Newtonsoft.Json;
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
