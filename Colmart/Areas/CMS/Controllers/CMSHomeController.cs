using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColmartCMS.Controllers
{
    public class CMSHomeController : Controller
    {
        public ActionResult Dashboard()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}