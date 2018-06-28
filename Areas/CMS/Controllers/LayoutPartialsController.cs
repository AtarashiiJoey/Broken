using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColmartCMS.View_Models.LayoutPartials;
using Colmart.Model_Manager;
using Colmart.Models;

namespace ColmartCMS.Controllers
{
    public class LayoutPartialsController : Controller
    {
        // GET: LayoutPartials
        public ActionResult SearchBox()
        {
            return PartialView();
        }
        public ActionResult OptionalTopRightNotificationsMenu()
        {
            return PartialView();
        }

        public ActionResult CMSUserDropDownMenuBox()
        {
            clsCMSUserDropDownMenuBox clsCMSUserDropDownMenuBox = new clsCMSUserDropDownMenuBox();
            clsCMSUsers clsCMSUser;
            if (Session["clsCMSUser"] != null)
            {
                string strImagePath = "";
                clsCMSUser = (clsCMSUsers)Session["clsCMSUser"];
                clsCMSUserDropDownMenuBox.strFullName = clsCMSUser.strFirstName + " " + clsCMSUser.strSurname;
                clsCMSUserDropDownMenuBox.strEmailAddress = clsCMSUser.strEmailAddress;
                if (clsCMSUser.strImagePath != null && clsCMSUser.strImageName != null && clsCMSUser.strImagePath != "" && clsCMSUser.strImageName != "")
                {
                    strImagePath = clsCMSUser.strImagePath + "\\" + clsCMSUser.strImageName;
                    clsCMSUserDropDownMenuBox.strImagePath = "/" + strImagePath.Replace("\\", "/");
                    clsCMSUserDropDownMenuBox.strLockScreenImagePath = "/" + strImagePath.Replace("\\", "/");
                }
                    
                clsCMSUserDropDownMenuBox.strRoleType = clsCMSUser.clsCMSRoleType.strTitle;
            }            

            return PartialView(clsCMSUserDropDownMenuBox);
        }

        public ActionResult Logout()
        {
            //Clear session
            Session["clsCMSUser"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}