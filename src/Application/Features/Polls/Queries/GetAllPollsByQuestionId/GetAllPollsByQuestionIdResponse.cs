using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp.Application.Features.Polls.Queries.GetAllPollsByQuestionId
{
    public class GetAllPollsByQuestionIdResponse
    {
        public int GreenVotes { get; set; }
        public int RedVotes { get; set; }
        public DateTime Started { get; set; }
        public DateTime Stopped { get; set; }
    }
}
