using AutoMapper;
using VoteApp.Application.Requests.Identity;
using VoteApp.Application.Responses.Identity;

namespace VoteApp.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}