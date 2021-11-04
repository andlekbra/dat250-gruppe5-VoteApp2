using VoteApp.Application.Interfaces.Repositories;
using VoteApp.Domain.Entities.Catalog;

namespace VoteApp.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand, int> _repository;

        public BrandRepository(IRepositoryAsync<Brand, int> repository)
        {
            _repository = repository;
        }
    }
}