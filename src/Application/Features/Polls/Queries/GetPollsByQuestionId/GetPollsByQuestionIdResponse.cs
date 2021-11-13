using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp.Application.Features.Polls.Queries.GetAllPollsByQuestionId
{
    public class GetPollsByQuestionIdResponse
    {
        public int Id { get; set; }
        public string JoinCode { get; set; }
        public int GreenVotes { get; set; }
        public int RedVotes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string Question { get; set; }
        public string RedAnswer { get; set; }
        public string GreenAnswer { get; set; }
    }
}
