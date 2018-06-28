using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Colmart.Models;

namespace ColmartCMS.View_Models.Users
{
    public class clsUserAdd
    {
        public clsUserAdd()
        {
            clsUser = new clsUsers();
        }
        public clsUsers clsUser { get; set; }
        public List<clsRoleTypes> lstRoleTypes { get; set; }

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
