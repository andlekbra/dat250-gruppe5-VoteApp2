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
        private readonly ILogger<RabbitJob> _logger;
        //private readonly MyDbContext _dbContext;

        public RabbitJob(ILogger<RabbitJob> logger)//, MyDbContext dbContext)
        {
            _logger = logger;
            //_dbContext = dbContext;
        }

        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await RunAtTimeOf(DateTime.Now);

        }

        public async Task RunAtTimeOf(DateTime now)
        {
            _logger.LogInformation("Job starts...");
            //do some work
            //await _dbContext.SaveChangesAsync
            //
            //Test("testy");
            Poll poll = new Poll();
            Test2(poll);
            _logger.LogInformation("Job completed");

        }


        public static void Test(string args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                var message = GetMessage(new string[] { args });
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "logs",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine("Sendt");
            }
        }



            public static void Test2(Poll poll)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                var json = JsonConvert.SerializeObject(poll);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "logs",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine("Sendt");
            }

            //Console.WriteLine(" Press [enter] to exit.");
            //Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "info: Test");
        }
    }

    
    /*

    public class PollResultSender : IPollResultSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _queueName;
        private readonly string _userName;
        private IConnection _connection;

        public PollResultSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _queueName = rabbitMqOptions.Value.QueueName;
            _hostName = rabbitMqOptions.Value.HostName;
            _userName = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            CreateConnection();

        }



        public void SendPollResult(Poll poll)

        {
            if (ConnectionExsists())
            {
                using (var channel = _connection.CreateModel())
                {

                    channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var json = JsonConvert.SerializeObject(poll);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body = body);

                }
            }
        }


        private void CreateConnection()
        {

            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };
                _connection = factory.CreateConnection();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create connection: {ex.Message}");
            }
        }


        private bool ConnectionExsists()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateConnection();

            return _connection != null;
        }
    }
       */
      
}
