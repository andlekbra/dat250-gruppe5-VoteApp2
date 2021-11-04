using VoteApp.Application.Responses.Identity;
using VoteApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Chat;
using VoteApp.Application.Models.Chat;

namespace VoteApp.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}