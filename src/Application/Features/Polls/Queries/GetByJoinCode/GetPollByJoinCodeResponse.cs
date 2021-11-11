﻿using System;

namespace VoteApp.Application.Features.Polls.Queries.GetByJoinCode
{
    class GetPollByJoinCodeResponse
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string JoinCode { get; set; }
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string Question { get; set; }
        public string RedAnswer { get; set; }
        public string GreenAnswer { get; set; }

    }
}
