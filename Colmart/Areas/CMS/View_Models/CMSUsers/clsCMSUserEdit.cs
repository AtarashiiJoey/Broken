using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colmart.Models;

namespace ColmartCMS.View_Models.CMSUsers
{
    public class clsCMSUserEdit
    {
        public clsCMSUserEdit()
        {
            clsCMSUser = new clsCMSUsers();
        }
        public clsCMSUsers clsCMSUser { get; set; }
        public List<clsCMSRoleTypes> lstCMSRoleTypes { get; set; }
        public string strEmailAddress { get; set; }
        public string strFullImagePath { get; set; }
        public string strCMSRoleType { get; set; }
        public bool bResetPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters long")]
        public string strNewPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters long")]
        [Compare("strNewPassword", ErrorMessage = "Password does not match")]
        public string strConfirmPassword { get; set; }

        public string strCropImageData { get; set; }
        public string strCropImageName { get; set; }
    }
}
