using VoteApp.Shared.Settings;
using System.Threading.Tasks;
using VoteApp.Shared.Wrapper;

namespace VoteApp.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}