using EasyNetQ;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;

namespace Subscriber
{
    internal class Program
    {

        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("amqps://cfuzuohh:ECv7Ll8eRfRzcJK9ITYbCtm97Yo3gXPP@hawk.rmq.cloudamqp.com/cfuzuohh"))

            {

                bus.PubSub.SubscribeAsync<string>("VoteApp", HandleTextJson);
                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();

            }

            static void HandleTextJson(string json)
            {
                if (json is null)
                {
                    throw new ArgumentNullException(nameof(json));
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Got message: {0}", json);
                Console.ResetColor();
                AddToDb(json);
            }

            static async void AddToDb(string json)
            {
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("test");
                var _collection = database.GetCollection<BsonDocument>("test");
                var bson = BsonSerializer.Deserialize<BsonDocument>(json);
                await _collection.InsertOneAsync(bson).ConfigureAwait(false);
            }
        }
    }
}
