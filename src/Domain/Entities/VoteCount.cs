using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApp.Domain.Contracts;

namespace VoteApp.Domain.Entities
{
    public class VoteCount : AuditableEntity<int>
    {
        public int RedVotes { get; set; }
        public int GreenVotes { get; set; }
    }
}
