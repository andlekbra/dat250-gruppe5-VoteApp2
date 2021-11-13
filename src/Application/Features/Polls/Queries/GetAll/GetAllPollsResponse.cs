﻿using System;

namespace VoteApp.Application.Features.Polls.Queries.GetAll
{
    public class GetAllPollsResponse
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public string JoinCode { get; set; }
        public int PollQuestionId { get; set; }
    }
}
