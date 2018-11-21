using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using ColmartCMS.View_Models.RoleTypes;
using Colmart.Model_Manager;
using Colmart;

namespace ColmartCMS.Controllers
{
    public class RoleTypesController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();

        // GET: RoleTypes
        public ActionResult RoleTypesView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsRoleTypesView clsRoleTypesView = new clsRoleTypesView();
            clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager();
            clsRoleTypesView.lstRoleTypes = clsRoleTypesManager.getAllRoleTypesOnlyList();

            return View(clsRoleTypesView);
        }

        public ActionResult RoleTypeAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsRoleTypes clsRoleType = new clsRoleTypes();

            return View(clsRoleType);
        }

        //Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleTypeAdd(clsRoleTypes clsRoleType)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager();
            clsRoleTypesManager.saveRoleType(clsRoleType);

            //Add successful / notification
            TempData["bIsRoleTypeAdded"] = true;

            return RedirectToAction("RoleTypesView", "RoleTypes");
        }

        //Edit
        public ActionResult RoleTypeEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("RoleTypesView", "RoleTypes");
            }

            clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager();
            clsRoleTypes clsRoleType = clsRoleTypesManager.getRoleTypeByID(id);

            return View(clsRoleType);
        }

        //Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleTypeEdit(clsRoleTypes clsRoleType)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager();
            clsRoleTypes clsExistingRoleType = clsRoleTypesManager.getRoleTypeByID(clsRoleType.iRoleTypeID);

            clsExistingRoleType.strTitle = clsRoleType.strTitle;
            clsRoleTypesManager.saveRoleType(clsExistingRoleType);

            //Add successful / notification
            TempData["bIsRoleTypeUpdated"] = true;

            return RedirectToAction("RoleTypesView", "RoleTypes");
        }

        //Delete
        [HttpPost]
        public ActionResult RoleTypeDelete(int iRoleTypeID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iRoleTypeID == 0)
            {
                return RedirectToAction("RoleTypesView", "RoleTypes");
            }

            clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager();
            clsRoleTypesManager.removeRoleTypeByID(iRoleTypeID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsRoleTypeDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Check if exists
        [HttpPost]
        public JsonResult checkIfRoleTypeExists(string strTitle)
        {
            bool bCanUseTitle = false;
            bool bExists = db.tblRoleTypes.Any(RoleType => RoleType.strTitle.ToLower() == strTitle.ToLower() && RoleType.bIsDeleted == false);

            if (bExists == false)
                bCanUseTitle = true;

            return Json(bCanUseTitle, JsonRequestBehavior.AllowGet);
        }
    }
}