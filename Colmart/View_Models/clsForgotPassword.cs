using System.ComponentModel.DataAnnotations;

namespace Colmart.View_Models
{
    /// <summary>
    /// Model and properties for clsForgotPassword
    /// </summary>
    public class clsForgotPassword
    {
        [Required]
        public string strEmail { get; set; }
    }
}