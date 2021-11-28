using AutoMapper;
using VoteApp.Application.Features.Polls.Commands.Add;
using VoteApp.Application.Features.Polls.Queries.GetAll;
using VoteApp.Application.Features.Polls.Queries.GetAllPollsByQuestionId;
using VoteApp.Domain.Entities;

namespace VoteApp.Application.Mappings
{
    class PollProfile : Profile
    {
        public PollProfile()
        {
            CreateMap<AddPollCommand, Poll>().ReverseMap();
            CreateMap<GetAllPollsResponse, Poll>().ReverseMap();
            CreateMap<GetPollsByQuestionIdResponse, Poll>().ReverseMap();


        }
    }
}
