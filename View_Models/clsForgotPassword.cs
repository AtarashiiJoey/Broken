using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Colmart.View_Models
{
    public class clsForgotPassword
    {
        [Required]
        public string strEmail { get; set; }
    }
}