using System.Threading.Tasks;

namespace VoteApp.Application.Interfaces.Services
{
    public interface IDweetService
    {
        public Task Dweet(string JSONcontent);
    }
}
