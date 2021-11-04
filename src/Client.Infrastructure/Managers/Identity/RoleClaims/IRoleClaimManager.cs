using System.Collections.Generic;
using System.Threading.Tasks;
using VoteApp.Application.Requests.Identity;
using VoteApp.Application.Responses.Identity;
using VoteApp.Shared.Wrapper;

namespace VoteApp.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}