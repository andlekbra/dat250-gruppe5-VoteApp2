using VoteApp.Domain.Contracts;
using VoteApp.Domain.Entities.Misc;

namespace VoteApp.Domain.Entities.ExtendedAttributes
{
    public class DocumentExtendedAttribute : AuditableEntityExtendedAttribute<int, int, Document>
    {
    }
}