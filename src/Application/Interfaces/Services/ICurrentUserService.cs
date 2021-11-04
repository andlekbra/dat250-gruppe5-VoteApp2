using VoteApp.Application.Interfaces.Common;

namespace VoteApp.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}