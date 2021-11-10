using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp.Application.Features.PollQuestions.Queries.GetAll
{
    public class GetAllPollQuestionsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string RedAnswer { get; set; }
        public string GreenAnswer { get; set; }
    }
}
