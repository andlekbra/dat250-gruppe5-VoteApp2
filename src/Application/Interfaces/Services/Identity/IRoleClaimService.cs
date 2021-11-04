using System.Collections.Generic;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Common;
using VoteApp.Application.Requests.Identity;
using VoteApp.Application.Responses.Identity;
using VoteApp.Shared.Wrapper;

namespace VoteApp.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}