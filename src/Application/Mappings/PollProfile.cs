using AutoMapper;
using VoteApp.Application.Features.Polls.Commands.Add;
using VoteApp.Application.Features.Polls.Queries.GetAll;
using VoteApp.Application.Features.Polls.Queries.GetAllPollsByQuestionId;
using VoteApp.Domain.Entities.Vote;

namespace VoteApp.Application.Mappings
{
    class PollProfile : Profile
    {
        public PollProfile()
        {
            CreateMap<AddPollCommand, Poll>().ReverseMap();
            CreateMap<GetPollByIdResponse, Poll>().ReverseMap();
            CreateMap<GetPollsByQuestionIdResponse, Poll>().ReverseMap();


        }
    }
}
