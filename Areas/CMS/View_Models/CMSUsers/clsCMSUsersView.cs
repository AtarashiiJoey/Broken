using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Colmart.Model_Manager;
using Colmart.Models;
using ColmartCMS.View_Models;
using ColmartCMS.View_Models.Users;

namespace ColmartCMS.View_Models.CMSUsers
{
    public class clsCMSUsersView
    {
        public List<clsCMSUsers> lstCMSUsers { get; set; }
        public int iCMSUserID { get; set; }
    }
}
