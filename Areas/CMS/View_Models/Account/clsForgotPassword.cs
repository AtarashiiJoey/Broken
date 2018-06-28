
using System.ComponentModel.DataAnnotations;

namespace ColmartCMS.View_Models.Account
{
    public class clsForgotPassword
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string strEmail { get; set; }
    }
}
