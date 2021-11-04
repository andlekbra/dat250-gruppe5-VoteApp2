using VoteApp.Application.Requests;

namespace VoteApp.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}