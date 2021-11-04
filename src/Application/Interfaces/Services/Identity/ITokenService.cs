using VoteApp.Application.Interfaces.Common;
using VoteApp.Application.Requests.Identity;
using VoteApp.Application.Responses.Identity;
using VoteApp.Shared.Wrapper;
using System.Threading.Tasks;

namespace VoteApp.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}