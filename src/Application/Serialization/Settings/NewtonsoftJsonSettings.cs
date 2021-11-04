
using VoteApp.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace VoteApp.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}