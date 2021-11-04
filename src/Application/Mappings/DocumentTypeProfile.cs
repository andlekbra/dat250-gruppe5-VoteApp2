using AutoMapper;
using VoteApp.Application.Features.DocumentTypes.Commands.AddEdit;
using VoteApp.Application.Features.DocumentTypes.Queries.GetAll;
using VoteApp.Application.Features.DocumentTypes.Queries.GetById;
using VoteApp.Domain.Entities.Misc;

namespace VoteApp.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}