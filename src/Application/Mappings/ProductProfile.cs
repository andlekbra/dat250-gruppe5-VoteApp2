using AutoMapper;
using VoteApp.Application.Features.Products.Commands.AddEdit;
using VoteApp.Domain.Entities.Catalog;

namespace VoteApp.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}