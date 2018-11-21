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
    public class clsUserAccessAdd
    {
        public clsUserAccessAdd()
        {
            clsUserAccess = new clsUserAccess();
        }
        public clsUserAccess clsUserAccess { get; set; }
        public List<clsUsers> lstUsers { get; set; }
        public List<clsPages> lstPages { get; set; }

    }
}
