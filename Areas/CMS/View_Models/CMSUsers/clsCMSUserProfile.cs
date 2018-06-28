using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colmart.Model_Manager;
using Colmart.Models;

namespace ColmartCMS.View_Models.CMSUsers
{
    public class clsCMSUserProfile
    {
        public clsCMSUserProfile()
        {
            clsCMSUser = new clsCMSUsers();
        }
        public clsCMSUsers clsCMSUser { get; set; }
        public string strFullImagePath { get; set; }
        public string strFullName { get; set; }
    }
}
