using AutoMapper;
using VoteApp.Application.Interfaces.Chat;
using VoteApp.Application.Models.Chat;
using VoteApp.Infrastructure.Models.Identity;

namespace VoteApp.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}