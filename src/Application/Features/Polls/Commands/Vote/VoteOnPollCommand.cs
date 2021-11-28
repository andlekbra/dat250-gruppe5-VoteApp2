using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using VoteApp.Domain.Entities;


namespace VoteApp.Application.Features.Polls.Commands.Vote
{
    public class VoteOnPollCommand : IRequest<Result<int>>
    {
        public string JoinCode { get; set; }
        public VoteCount VoteCount { get; set; }

        public VoteOnPollCommand(string JoinCode, VoteCount voteCount)
		{
            this.JoinCode = JoinCode;
            this.VoteCount = voteCount;
		}
        public VoteOnPollCommand()
		{

		}
    }

    internal class VoteOnPollCommandHandler : IRequestHandler<VoteOnPollCommand, Result<int>>
    {
       // private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        //private readonly IStringLocalizer<VoteOnPollCommandHandler> _localizer;

        public VoteOnPollCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_mapper = mapper;
           // _localizer = localizer;
        }

        public async Task<Result<int>> Handle(VoteOnPollCommand command, CancellationToken cancellationToken)
        {
            //Todo
            //If ongoing poll with joincode exists update votecount (or just post votecount to avoid race conditions)
            //Else return error message
            Poll poll = await _unitOfWork.Repository<Poll>().Entities.Where(p => (p.StopTime == null) && (p.JoinCode == command.JoinCode)).Include(p => p.VoteCount).FirstOrDefaultAsync();

            if (poll != null)
            {
                Console.WriteLine("SUCCESS");
                //return await Result<int>.FailAsync(_localizer["JoinCode already exists."]);
            } else
			{
                Console.WriteLine("FAILED");
                return await Result<int>.FailAsync("JoinCode does not exist");
            }

            var voteCount = await _unitOfWork.Repository<VoteCount>().GetByIdAsync(poll.VoteCount.Id);

            voteCount.RedVotes += command.VoteCount.RedVotes;
            voteCount.GreenVotes += command.VoteCount.GreenVotes;



            await _unitOfWork.Repository<VoteCount>().UpdateAsync(voteCount);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(command.VoteCount.Id, "VoteCount Saved");

            //return await Result<int>.SuccessAsync();
            
        }
    }
}
