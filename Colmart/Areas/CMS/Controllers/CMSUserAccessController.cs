using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using ColmartCMS.View_Models.CMSUserAccess;
using Colmart.Model_Manager;
using Colmart;

namespace ColmartCMS.Controllers
{
    public class CMSUserAccessController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();

        // GET: CMSUserAccess
        public ActionResult CMSUserAccessView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSUserAccessView clsCMSUserAccessView = new clsCMSUserAccessView();
            clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager();
            clsCMSUserAccessView.lstCMSUserAccess = clsCMSUserAccessManager.getAllCMSUserAccessList();

            return View(clsCMSUserAccessView);
        }

        public ActionResult CMSUserAccessAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSUserAccessAdd clsCMSUserAccessAdd = new clsCMSUserAccessAdd();
            clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();
            clsCMSPagesManager clsCMSPagesManager = new clsCMSPagesManager();
            
            clsCMSUserAccessAdd.clsCMSUserAccess = new clsCMSUserAccess();
            clsCMSUserAccessAdd.lstCMSUsers = clsCMSUsersManager.getAllCMSUsersList();

            //Get full names
            if (clsCMSUserAccessAdd.lstCMSUsers.Count > 0)
                foreach (var item in clsCMSUserAccessAdd.lstCMSUsers)
                    item.strFirstName = item.strFirstName + " " + item.strSurname;
            clsCMSUserAccessAdd.lstCMSPages = clsCMSPagesManager.getAllCMSPagesList();

            return View(clsCMSUserAccessAdd);
        }

        //Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CMSUserAccessAdd(clsCMSUserAccessAdd clsCMSUserAccessAdd)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool dDoesRecordExist = db.tblCMSUserAccess.Any(CMSUserAccess => CMSUserAccess.iCMSUserID == clsCMSUserAccessAdd.clsCMSUserAccess.iCMSUserID
                                    && CMSUserAccess.iCMSPageID == clsCMSUserAccessAdd.clsCMSUserAccess.iCMSPageID
                                    && CMSUserAccess.bIsDeleted == false);
            if (dDoesRecordExist == true)
            {
                TempData["bIsCMSUserAccessRecordExists"] = true;
                clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();
                clsCMSPagesManager clsCMSPagesManager = new clsCMSPagesManager();

                clsCMSUserAccessAdd.clsCMSUserAccess = new clsCMSUserAccess();
                clsCMSUserAccessAdd.lstCMSUsers = clsCMSUsersManager.getAllCMSUsersList();

                //Get full names
                if (clsCMSUserAccessAdd.lstCMSUsers.Count > 0)
                    foreach (var item in clsCMSUserAccessAdd.lstCMSUsers)
                        item.strFirstName = item.strFirstName + " " + item.strSurname;
                clsCMSUserAccessAdd.lstCMSPages = clsCMSPagesManager.getAllCMSPagesList();
                return View(clsCMSUserAccessAdd);
            }

            clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager();
            clsCMSUserAccessManager.saveCMSUserAccess(clsCMSUserAccessAdd.clsCMSUserAccess);

            //Add successful / notification
            TempData["bIsCMSUserAccessAdded"] = true;

            return RedirectToAction("CMSUserAccessView", "CMSUserAccess");
        }

        //Edit
        public ActionResult CMSUserAccessEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("CMSUserAccessView", "CMSUserAccess");
            }
            clsCMSUserAccessEdit clsCMSUserAccessEdit = new clsCMSUserAccessEdit();
            clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager();
            clsCMSUserAccessEdit.clsCMSUserAccess = clsCMSUserAccessManager.getCMSUserAccessByID(id);

            clsCMSUserAccessEdit.iCMSUserID = clsCMSUserAccessEdit.clsCMSUserAccess.clsCMSUser.iCMSUserID;
            clsCMSUserAccessEdit.iCMSPageID = clsCMSUserAccessEdit.clsCMSUserAccess.clsCMSPage.iCMSPageID;
            clsCMSUserAccessEdit.strFullName = clsCMSUserAccessEdit.clsCMSUserAccess.clsCMSUser.strFirstName + " " + clsCMSUserAccessEdit.clsCMSUserAccess.clsCMSUser.strSurname;
            clsCMSUserAccessEdit.strPageName = clsCMSUserAccessEdit.clsCMSUserAccess.clsCMSPage.strTitle;

            return View(clsCMSUserAccessEdit);
        }

        //Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CMSUserAccessEdit(clsCMSUserAccessEdit clsCMSUserAccessEdit)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager();
            clsCMSUserAccess clsExistingCMSUserAccess = clsCMSUserAccessManager.getCMSUserAccessByID(clsCMSUserAccessEdit.clsCMSUserAccess.iCMSUserAccessID);

            clsExistingCMSUserAccess.bIsRead = clsCMSUserAccessEdit.clsCMSUserAccess.bIsRead;
            clsExistingCMSUserAccess.bIsWrite = clsCMSUserAccessEdit.clsCMSUserAccess.bIsWrite;

            clsCMSUserAccessManager.saveCMSUserAccess(clsExistingCMSUserAccess);

            //Add successful / notification
            TempData["bIsCMSUserAccessUpdated"] = true;

            return RedirectToAction("CMSUserAccessView", "CMSUserAccess");
        }

        //Delete
        [HttpPost]
        public ActionResult CMSUserAccessDelete(int iCMSUserAccessID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iCMSUserAccessID == 0)
            {
                return RedirectToAction("CMSUserAccessView", "CMSUserAccess");
            }

            clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager();
            clsCMSUserAccessManager.removeCMSUserAccessByID(iCMSUserAccessID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsCMSUserAccessDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }
    }
}