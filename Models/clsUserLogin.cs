using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Colmart.Models
{
    public class clsUserLogin
    {
        public string strEmail { get; set; }

        [DataType(DataType.Password)]
        public string strPassword { get; set; }

        public bool bRememberMe { get; set; }
    }
}