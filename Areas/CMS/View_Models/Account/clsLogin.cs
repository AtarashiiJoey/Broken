
using System.ComponentModel.DataAnnotations;

namespace ColmartCMS.View_Models.Account
{
    public class clsLogin
    {
        public string strEmail { get; set; }

        [DataType(DataType.Password)]
        public string strPassword { get; set; }

        public bool bRememberMe { get; set; }
    }
}
