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

namespace VoteApp.Application.Features.Polls.Commands.Add
{
    public partial class AddPollCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? StopTime { get; set; }
        [Required]
        public string JoinCode { get; set; }
        [Required]
        public int PollQuestionId { get; set; }
    }

    internal class AddPollCommandHandler : IRequestHandler<AddPollCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<AddPollCommandHandler> _localizer;

        public AddPollCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddPollCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddPollCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Poll>().Entities.Where(p => p.Id != command.Id)
                .AnyAsync(p => p.JoinCode == command.JoinCode, cancellationToken))
            {
                return await Result<int>.FailAsync(_localizer["JoinCode already exists."]);
            }
            
            var poll = _mapper.Map<Poll>(command);

            poll.StopTime = DateTime.MinValue;
            poll.StartTime = DateTime.Now;

            await _unitOfWork.Repository<Poll>().AddAsync(poll);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(poll.Id, _localizer["Poll Saved"]);
            
        }
    }
}
