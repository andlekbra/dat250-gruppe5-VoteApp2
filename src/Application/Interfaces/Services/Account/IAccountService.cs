using VoteApp.Application.Interfaces.Common;
using VoteApp.Application.Requests.Identity;
using VoteApp.Shared.Wrapper;
using System.Threading.Tasks;

namespace VoteApp.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}