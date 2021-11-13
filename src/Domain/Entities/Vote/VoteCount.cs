using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp.Domain.Entities.Vote
{
    public class VoteCount
    {
        public int Id { get; set; }
        public int RedVotes { get; set; }
        public int GreenVotes { get; set; }
    }
}
