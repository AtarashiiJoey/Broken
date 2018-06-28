using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using ColmartCMS.View_Models.CMSRoleTypes;
using Colmart.Model_Manager;
using ColmartCMS;
using Colmart;

namespace ColmartCMS.Controllers
{
    public class CMSRoleTypesController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();

        // GET: CMSRoleTypes
        public ActionResult CMSRoleTypesView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSRoleTypesView clsCMSRoleTypesView = new clsCMSRoleTypesView();
            clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager();
            clsCMSRoleTypesView.lstCMSRoleTypes = clsCMSRoleTypesManager.getAllCMSRoleTypesOnlyList();

            return View(clsCMSRoleTypesView);
        }

        public ActionResult CMSRoleTypeAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSRoleTypes clsCMSRoleType = new clsCMSRoleTypes();

            return View(clsCMSRoleType);
        }

        //Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CMSRoleTypeAdd(clsCMSRoleTypes clsCMSRoleType)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager();
            clsCMSRoleTypesManager.saveCMSRoleType(clsCMSRoleType);

            //Add successful / notification
            TempData["bIsCMSRoleTypeAdded"] = true;

            return RedirectToAction("CMSRoleTypesView", "CMSRoleTypes");
        }

        //Edit
        public ActionResult CMSRoleTypeEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("CMSRoleTypesView", "CMSRoleTypes");
            }

            clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager();
            clsCMSRoleTypes clsCMSRoleType = clsCMSRoleTypesManager.getCMSRoleTypeByID(id);

            return View(clsCMSRoleType);
        }

        //Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CMSRoleTypeEdit(clsCMSRoleTypes clsCMSRoleType)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager();
            clsCMSRoleTypes clsExistingCMSRoleType = clsCMSRoleTypesManager.getCMSRoleTypeByID(clsCMSRoleType.iCMSRoleTypeID);

            clsExistingCMSRoleType.strTitle = clsCMSRoleType.strTitle;
            clsCMSRoleTypesManager.saveCMSRoleType(clsExistingCMSRoleType);

            //Add successful / notification
            TempData["bIsCMSRoleTypeUpdated"] = true;

            return RedirectToAction("CMSRoleTypesView", "CMSRoleTypes");
        }

        //Delete
        [HttpPost]
        public ActionResult CMSRoleTypeDelete(int iCMSRoleTypeID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iCMSRoleTypeID == 0)
            {
                return RedirectToAction("CMSRoleTypesView", "CMSRoleTypes");
            }

            clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager();
            clsCMSRoleTypesManager.removeCMSRoleTypeByID(iCMSRoleTypeID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsCMSRoleTypeDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Check if exists
        [HttpPost]
        public JsonResult checkIfCMSRoleTypeExists(string strTitle)
        {
            bool bCanUseTitle = false;
            bool bExists = db.tblCMSRoleTypes.Any(CMSRoleType => CMSRoleType.strTitle.ToLower() == strTitle.ToLower() && CMSRoleType.bIsDeleted == false);

            if (bExists == false)
                bCanUseTitle = true;

            return Json(bCanUseTitle, JsonRequestBehavior.AllowGet);
        }
    }
}