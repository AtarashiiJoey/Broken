using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Colmart.Model_Manager;
using Colmart.Models;

namespace ColmartCMS.View_Models.Users
{
    public class clsUsersView
    {
        public List<clsUsers> lstUsers { get; set; }
        public int iUserID { get; set; }
    }
}
