using System.Text.Json;
using VoteApp.Application.Interfaces.Serialization.Options;

namespace VoteApp.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}