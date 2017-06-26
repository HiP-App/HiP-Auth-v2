using System.ComponentModel.DataAnnotations;

namespace PaderbornUniversity.SILab.Hip.Auth.Models.ManageViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
