using System.ComponentModel.DataAnnotations;
using AutoMapper;
using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using VoteApp.Domain.Entities.Vote;

namespace VoteApp.Application.Features.Polls.Commands.Vote
{
    public partial class VoteOnPollCommand : IRequest<Result<int>>
    {
        public string JoinCode { get; set; }
        public VoteCount VoteCount { get; set; }
    }

    internal class VoteOnPollCommandHandler : IRequestHandler<VoteOnPollCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<VoteOnPollCommandHandler> _localizer;

        public VoteOnPollCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<VoteOnPollCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(VoteOnPollCommand command, CancellationToken cancellationToken)
        {
            //Todo
           //If ongoing poll with joincode exists update votecount (or just post votecount to avoid race conditions)
           //Else return error message

            return await Result<int>.SuccessAsync();
            
        }
    }
}
