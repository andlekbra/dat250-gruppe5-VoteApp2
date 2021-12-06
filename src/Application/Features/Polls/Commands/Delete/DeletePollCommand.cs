using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using VoteApp.Domain.Entities;

namespace VoteApp.Application.Features.Polls.Commands.Del
{
    public class DeletePollCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeletePollCommandHandler : IRequestHandler<DeletePollCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<DeletePollCommandHandler> _localizer;

        public DeletePollCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeletePollCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeletePollCommand command, CancellationToken cancellationToken)
        {
            var poll = await _unitOfWork.Repository<Poll>().GetByIdAsync(command.Id);
            if (poll != null)
            {
                await _unitOfWork.Repository<Poll>().DeleteAsync(poll);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(poll.Id, _localizer["Poll Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Poll Not Found!"]);
            }
        }
    }
}
