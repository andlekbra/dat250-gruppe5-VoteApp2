using AutoMapper;
using VoteApp.Application.Features.Brands.Commands.AddEdit;
using VoteApp.Application.Features.Brands.Queries.GetAll;
using VoteApp.Application.Features.Brands.Queries.GetById;
using VoteApp.Domain.Entities.Catalog;

namespace VoteApp.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}