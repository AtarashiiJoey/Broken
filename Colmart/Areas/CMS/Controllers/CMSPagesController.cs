using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using ColmartCMS.View_Models.CMSPages;
using Colmart.Model_Manager;
using Colmart;

namespace ColmartCMS.Controllers
{
    public class CMSPagesController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();

        // GET: CMSPages
        public ActionResult CMSPagesView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSPagesView clsCMSPagesView = new clsCMSPagesView();
            clsCMSPagesManager clsCMSPagesManager = new clsCMSPagesManager();
            clsCMSPagesView.lstCMSPages = clsCMSPagesManager.getAllCMSPagesOnlyList();

            return View(clsCMSPagesView);
        }

        public ActionResult CMSPageAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSPages clsCMSPage = new clsCMSPages();

            return View(clsCMSPage);
        }

        //Add CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CMSPageAdd(clsCMSPages clsCMSPage)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSPagesManager clsCMSPagesManager = new clsCMSPagesManager();
            clsCMSPagesManager.saveCMSPage(clsCMSPage);

            //Add successful / notification
            TempData["bIsCMSPageAdded"] = true;

            return RedirectToAction("CMSPagesView", "CMSPages");
        }

        //Edit CMS Page
        public ActionResult CMSPageEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("CMSPagesView", "CMSPages");
            }

            clsCMSPagesManager clsCMSPagesManager = new clsCMSPagesManager();
            clsCMSPages clsCMSPage = clsCMSPagesManager.getCMSPageByID(id);

            return View(clsCMSPage);
        }

        //Edit CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CMSPageEdit(clsCMSPages clsCMSPage)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSPagesManager clsCMSPagesManager = new clsCMSPagesManager();
            clsCMSPages clsExistingCMSPage = clsCMSPagesManager.getCMSPageByID(clsCMSPage.iCMSPageID);

            clsExistingCMSPage.strTitle = clsCMSPage.strTitle;
            clsCMSPagesManager.saveCMSPage(clsExistingCMSPage);

            //Add successful / notification
            TempData["bIsCMSPageUpdated"] = true;

            return RedirectToAction("CMSPagesView", "CMSPages");
        }

        //Delete Page
        [HttpPost]
        public ActionResult CMSPageDelete(int iCMSPageID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iCMSPageID == 0)
            {
                return RedirectToAction("CMSPagesView", "CMSPages");
            }

            clsCMSPagesManager clsCMSPagesManager = new clsCMSPagesManager();
            clsCMSPagesManager.removeCMSPageByID(iCMSPageID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsCMSPageDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Check if exists
        [HttpPost]
        public JsonResult checkIfCMSPageExists(string strTitle)
        {
            bool bCanUseTitle = false;
            bool bExists = db.tblCMSPages.Any(CMSPage => CMSPage.strTitle.ToLower() == strTitle.ToLower() && CMSPage.bIsDeleted == false);

            if (bExists == false)
                bCanUseTitle = true;

            return Json(bCanUseTitle, JsonRequestBehavior.AllowGet);
        }
    }
}