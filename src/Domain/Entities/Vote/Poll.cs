using VoteApp.Domain.Contracts;
using System;

namespace VoteApp.Domain.Entities.Vote
{
    public class Poll : AuditableEntity<int>
    {
		public DateTime StartTime { get; set; }
		public DateTime StopTime { get; set; }
		public string JoinCode { get; set; }
        public PollQuestion Question { get; set; }
		public VoteCount VoteCount { get; set; }

	}
}
