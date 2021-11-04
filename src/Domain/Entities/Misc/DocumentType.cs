using VoteApp.Domain.Contracts;

namespace VoteApp.Domain.Entities.Misc
{
    public class DocumentType : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}