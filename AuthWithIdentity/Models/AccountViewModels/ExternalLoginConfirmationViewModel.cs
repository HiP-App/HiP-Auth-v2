using System.ComponentModel.DataAnnotations;

namespace PaderbornUniversity.SILab.Hip.Auth.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
