using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Colmart.Models;

namespace ColmartCMS.View_Models.CMSUserAccess
{
    public class clsCMSUserAccessAdd
    {
        public clsCMSUserAccessAdd()
        {
            clsCMSUserAccess = new clsCMSUserAccess();
        }
        public clsCMSUserAccess clsCMSUserAccess { get; set; }
        public List<clsCMSUsers> lstCMSUsers { get; set; }
        public List<clsCMSPages> lstCMSPages { get; set; }

    }
}
