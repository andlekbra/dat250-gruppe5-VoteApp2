using VoteApp.Application.Responses.Audit;
using VoteApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VoteApp.Client.Infrastructure.Managers.Audit
{
    public interface IAuditManager : IManager
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync();

        Task<IResult<string>> DownloadFileAsync(string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
    }
}