using VoteApp.Application.Models.Chat;
using VoteApp.Application.Responses.Identity;
using VoteApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Chat;

namespace VoteApp.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}