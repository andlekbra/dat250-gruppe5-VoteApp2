using AutoMapper;
using VoteApp.Infrastructure.Models.Audit;
using VoteApp.Application.Responses.Audit;

namespace VoteApp.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}