using AutoMapper;
using VoteApp.Infrastructure.Models.Identity;
using VoteApp.Application.Responses.Identity;

namespace VoteApp.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}