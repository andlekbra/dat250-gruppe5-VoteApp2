using System;

namespace VoteApp.Application.Features.Polls.Queries.GetAll
{
    public class GetPollByIdResponse
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public string JoinCode { get; set; }
        public int PollQuestionId { get; set; }

        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string Question { get; set; }
        public string GreenAnswer { get; set; }
        public string RedAnswer { get; set; }

    }
}
