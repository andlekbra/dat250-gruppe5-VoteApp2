using AutoMapper;
using VoteApp.Application.Features.Documents.Commands.AddEdit;
using VoteApp.Application.Features.Documents.Queries.GetById;
using VoteApp.Domain.Entities.Misc;

namespace VoteApp.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}