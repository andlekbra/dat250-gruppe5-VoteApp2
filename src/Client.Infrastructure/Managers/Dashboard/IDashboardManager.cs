using VoteApp.Shared.Wrapper;
using System.Threading.Tasks;
using VoteApp.Application.Features.Dashboards.Queries.GetData;

namespace VoteApp.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}