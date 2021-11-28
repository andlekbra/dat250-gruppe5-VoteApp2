using VoteApp.Domain.Contracts;

namespace VoteApp.Domain.Entities
{
    public class PollQuestion : AuditableEntity<int>
    { 
        public string Title { get; set; }
        public string Question { get; set; }
        public string RedAnswer { get; set; }
        public string GreenAnswer { get; set; }

    }
}
