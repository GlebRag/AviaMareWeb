using AviaMare.Models.CustomValidationAttrubites;
using System.ComponentModel.DataAnnotations;

namespace AviaMare.Models.Auth
{
    public class RegisterUserViewModel
    {
        [Required]
        [UniqUserName]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
