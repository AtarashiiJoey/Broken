using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Colmart.Models;

namespace ColmartCMS.View_Models.UserAccess
{
    public class clsUserAccessEdit
    {
        public clsUserAccessEdit()
        {
            clsUserAccess = new clsUserAccess();
        }
        public clsUserAccess clsUserAccess { get; set; }
        public string strFullName { get; set; }
        public string strPageName { get; set; }
        public int iUserID { get; set; }
        public int iPageID { get; set; }

    }
}
