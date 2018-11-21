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
    public class clsCMSUserAccessEdit
    {
        public clsCMSUserAccessEdit()
        {
            clsCMSUserAccess = new clsCMSUserAccess();
        }
        public clsCMSUserAccess clsCMSUserAccess { get; set; }
        public string strFullName { get; set; }
        public string strPageName { get; set; }
        public int iCMSUserID { get; set; }
        public int iCMSPageID { get; set; }

    }
}
