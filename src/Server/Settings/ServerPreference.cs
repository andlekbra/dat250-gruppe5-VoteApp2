using System.Linq;
using VoteApp.Shared.Constants.Localization;
using VoteApp.Shared.Settings;

namespace VoteApp.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}