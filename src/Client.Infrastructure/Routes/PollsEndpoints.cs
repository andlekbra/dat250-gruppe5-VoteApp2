namespace VoteApp.Client.Infrastructure.Routes
{
    public static class PollsEndpoints
    {
        public static string GetAll = "api/v1/poll";
        public static string Delete = "api/v1/poll";
        public static string Save = "api/v1/PollManagement/polls";
        public static string GetByQuestionId = "api/v1/PollManagement/poll-questions/{0}/polls";
    }
}
