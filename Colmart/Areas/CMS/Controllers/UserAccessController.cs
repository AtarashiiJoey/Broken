using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using ColmartCMS.View_Models.UserAccess;
using Colmart.Model_Manager;
using Colmart;

namespace ColmartCMS.Controllers
{
    public class UserAccessController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();

        // GET: UserAccess
        public ActionResult UserAccessView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsUserAccessView clsUserAccessView = new clsUserAccessView();
            clsUserAccessManager clsUserAccessManager = new clsUserAccessManager();
            clsUserAccessView.lstUserAccess = clsUserAccessManager.getAllUserAccessList();

            return View(clsUserAccessView);
        }

        public ActionResult UserAccessAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsUserAccessAdd clsUserAccessAdd = new clsUserAccessAdd();
            clsUsersManager clsUsersManager = new clsUsersManager();
            clsPagesManager clsPagesManager = new clsPagesManager();
            
            clsUserAccessAdd.clsUserAccess = new clsUserAccess();
            clsUserAccessAdd.lstUsers = clsUsersManager.getAllUsersList();

            //Get full names
            if (clsUserAccessAdd.lstUsers.Count > 0)
                foreach (var item in clsUserAccessAdd.lstUsers)
                    item.strFirstName = item.strFirstName + " " + item.strSurname;
            clsUserAccessAdd.lstPages = clsPagesManager.getAllPagesList();

            return View(clsUserAccessAdd);
        }

        //Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserAccessAdd(clsUserAccessAdd clsUserAccessAdd)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool dDoesRecordExist = db.tblUserAccess.Any(UserAccess => UserAccess.iUserID == clsUserAccessAdd.clsUserAccess.iUserID
                                    && UserAccess.iPageID == clsUserAccessAdd.clsUserAccess.iPageID
                                    && UserAccess.bIsDeleted == false);
            if (dDoesRecordExist == true)
            {
                TempData["bIsUserAccessRecordExists"] = true;
                clsUsersManager clsUsersManager = new clsUsersManager();
                clsPagesManager clsPagesManager = new clsPagesManager();

                clsUserAccessAdd.clsUserAccess = new clsUserAccess();
                clsUserAccessAdd.lstUsers = clsUsersManager.getAllUsersList();

                //Get full names
                if (clsUserAccessAdd.lstUsers.Count > 0)
                    foreach (var item in clsUserAccessAdd.lstUsers)
                        item.strFirstName = item.strFirstName + " " + item.strSurname;
                clsUserAccessAdd.lstPages = clsPagesManager.getAllPagesList();
                return View(clsUserAccessAdd);
            }

            clsUserAccessManager clsUserAccessManager = new clsUserAccessManager();
            clsUserAccessManager.saveUserAccess(clsUserAccessAdd.clsUserAccess);

            //Add successful / notification
            TempData["bIsUserAccessAdded"] = true;

            return RedirectToAction("UserAccessView", "UserAccess");
        }

        //Edit
        public ActionResult UserAccessEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("UserAccessView", "UserAccess");
            }
            clsUserAccessEdit clsUserAccessEdit = new clsUserAccessEdit();
            clsUserAccessManager clsUserAccessManager = new clsUserAccessManager();
            clsUserAccessEdit.clsUserAccess = clsUserAccessManager.getUserAccessByID(id);

            clsUserAccessEdit.iUserID = clsUserAccessEdit.clsUserAccess.clsUser.iUserID;
            clsUserAccessEdit.iPageID = clsUserAccessEdit.clsUserAccess.clsPage.iPageID;
            clsUserAccessEdit.strFullName = clsUserAccessEdit.clsUserAccess.clsUser.strFirstName + " " + clsUserAccessEdit.clsUserAccess.clsUser.strSurname;
            clsUserAccessEdit.strPageName = clsUserAccessEdit.clsUserAccess.clsPage.strTitle;

            return View(clsUserAccessEdit);
        }

        //Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserAccessEdit(clsUserAccessEdit clsUserAccessEdit)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsUserAccessManager clsUserAccessManager = new clsUserAccessManager();
            clsUserAccess clsExistingUserAccess = clsUserAccessManager.getUserAccessByID(clsUserAccessEdit.clsUserAccess.iUserAccessID);

            clsExistingUserAccess.bIsRead = clsUserAccessEdit.clsUserAccess.bIsRead;
            clsExistingUserAccess.bIsWrite = clsUserAccessEdit.clsUserAccess.bIsWrite;

            clsUserAccessManager.saveUserAccess(clsExistingUserAccess);

            //Add successful / notification
            TempData["bIsUserAccessUpdated"] = true;

            return RedirectToAction("UserAccessView", "UserAccess");
        }

        //Delete
        [HttpPost]
        public ActionResult UserAccessDelete(int iUserAccessID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iUserAccessID == 0)
            {
                return RedirectToAction("UserAccessView", "UserAccess");
            }

            clsUserAccessManager clsUserAccessManager = new clsUserAccessManager();
            clsUserAccessManager.removeUserAccessByID(iUserAccessID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsUserAccessDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }
    }
}