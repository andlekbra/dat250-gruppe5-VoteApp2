using VoteApp.Application.Features.Documents.Commands.AddEdit;
using VoteApp.Application.Features.Documents.Queries.GetAll;
using VoteApp.Application.Requests.Documents;
using VoteApp.Shared.Wrapper;
using System.Threading.Tasks;
using VoteApp.Application.Features.Documents.Queries.GetById;

namespace VoteApp.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}