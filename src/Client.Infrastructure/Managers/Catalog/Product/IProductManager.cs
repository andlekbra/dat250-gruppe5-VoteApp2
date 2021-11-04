using VoteApp.Application.Features.Products.Commands.AddEdit;
using VoteApp.Application.Features.Products.Queries.GetAllPaged;
using VoteApp.Application.Requests.Catalog;
using VoteApp.Shared.Wrapper;
using System.Threading.Tasks;

namespace VoteApp.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}