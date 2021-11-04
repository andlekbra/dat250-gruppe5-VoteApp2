using System.ComponentModel.DataAnnotations;

namespace VoteApp.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}