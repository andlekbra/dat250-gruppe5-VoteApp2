namespace VoteApp.Client.Infrastructure.Routes
{
    public static class PollsEndpoints
    {
        public static string GetAll = "api/v1/poll";
        public static string Delete = "api/v1/poll";
        public static string Save = "api/v1/PollManagement/polls";
        public static string GetByQuestionId = "api/v1/PollManagement/poll-questions/{0}/polls";
        public static string GetById = "api/v1/PollManagement/polls/{0}";
        public static string GetByJoinCode = "api/v1/OngoingPolls/{0}";
        public static string VoteByJoinCode = "api/v1/OngoingPolls/{0}/votecount";
        public static string Stop = "api/v1/PollManagement/polls/{0}/stop";
    }
}
