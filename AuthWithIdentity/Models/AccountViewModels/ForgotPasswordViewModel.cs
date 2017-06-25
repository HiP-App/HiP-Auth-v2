using System.ComponentModel.DataAnnotations;

namespace PaderbornUniversity.SILab.Hip.Auth.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
