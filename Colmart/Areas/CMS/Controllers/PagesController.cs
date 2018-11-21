using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using ColmartCMS.View_Models.Pages;
using Colmart.Model_Manager;
using Colmart;

namespace ColmartCMS.Controllers
{
    public class PagesController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();

        // GET: Pages
        public ActionResult PagesView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsPagesView clsPagesView = new clsPagesView();
            clsPagesManager clsPagesManager = new clsPagesManager();
            clsPagesView.lstPages = clsPagesManager.getAllPagesOnlyList();

            return View(clsPagesView);
        }

        public ActionResult PageAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsPages clsPage = new clsPages();

            return View(clsPage);
        }

        //Add CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PageAdd(clsPages clsPage)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsPagesManager clsPagesManager = new clsPagesManager();
            clsPagesManager.savePage(clsPage);

            //Add successful / notification
            TempData["bIsPageAdded"] = true;

            return RedirectToAction("PagesView", "Pages");
        }

        //Edit CMS Page
        public ActionResult PageEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("PagesView", "Pages");
            }

            clsPagesManager clsPagesManager = new clsPagesManager();
            clsPages clsPage = clsPagesManager.getPageByID(id);

            return View(clsPage);
        }

        //Edit CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PageEdit(clsPages clsPage)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsPagesManager clsPagesManager = new clsPagesManager();
            clsPages clsExistingPage = clsPagesManager.getPageByID(clsPage.iPageID);

            clsExistingPage.strTitle = clsPage.strTitle;
            clsPagesManager.savePage(clsExistingPage);

            //Add successful / notification
            TempData["bIsPageUpdated"] = true;

            return RedirectToAction("PagesView", "Pages");
        }

        //Delete Page
        [HttpPost]
        public ActionResult PageDelete(int iPageID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iPageID == 0)
            {
                return RedirectToAction("PagesView", "Pages");
            }

            clsPagesManager clsPagesManager = new clsPagesManager();
            clsPagesManager.removePageByID(iPageID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsPageDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Check if exists
        [HttpPost]
        public JsonResult checkIfPageExists(string strTitle)
        {
            bool bCanUseTitle = false;
            bool bExists = db.tblPages.Any(Page => Page.strTitle.ToLower() == strTitle.ToLower() && Page.bIsDeleted == false);

            if (bExists == false)
                bCanUseTitle = true;

            return Json(bCanUseTitle, JsonRequestBehavior.AllowGet);
        }
    }
}