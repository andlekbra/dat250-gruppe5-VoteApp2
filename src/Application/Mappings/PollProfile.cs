using AutoMapper;
using VoteApp.Application.Features.Polls.Commands.AddEdit;
using VoteApp.Domain.Entities.Vote;

namespace VoteApp.Application.Mappings
{
    class PollProfile : Profile
    {
        public PollProfile()
        {
            CreateMap<AddEditPollCommand, Poll>().ReverseMap();
        }
    }
}
