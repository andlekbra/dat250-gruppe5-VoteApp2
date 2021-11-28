using AutoMapper;
using VoteApp.Infrastructure.Models.Identity;
using VoteApp.Application.Responses.Identity;

namespace VoteApp.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, BlazorHeroUser>().ReverseMap();
        }
    }
}