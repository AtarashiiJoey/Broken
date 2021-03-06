﻿using Colmart.Model_Manager;
using Colmart.Models;
using Colmart.View_Models;
using ColmartCMS.Assistant_Classes;
using System.Linq;
using System.Web.Mvc;

namespace Colmart.Controllers
{
    public class ProfileController : Controller
    {
        /// <summary>
        /// Gets Account details from DB?
        /// </summary>
        /// <returns>ActionResult as View</returns>
        public ActionResult Account()
        {
            if (Session["clsUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var clsUsers = new clsUsers();
            clsUsers = Session["clsUser"] as clsUsers;
            var clsAccountUpdate = new clsAccountUpdate();
            var clsUsersManager = new clsUsersManager();
            clsAccountUpdate = clsUsersManager.getUserAccountById(clsUsers.iUserID);
            return View(clsAccountUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Account(clsAccountUpdate clsAccountUpdate)
        {
            if (ModelState.IsValid)
            {
                var clsUsers = new clsUsers
                {
                    iUserID = clsAccountUpdate.iUserID,
                    dtAdded = clsAccountUpdate.dtAdded,
                    iAddedBy = clsAccountUpdate.iAddedBy,
                    iEditedBy = clsAccountUpdate.iEditedBy,
                    iRoleTypeID = clsAccountUpdate.iRoleTypeID,
                    strFirstName = clsAccountUpdate.strFirstName,
                    strSurname = clsAccountUpdate.strSurname,
                    iAreaID = clsAccountUpdate.iAreaID,
                    strPrimaryContactNumber = clsAccountUpdate.strPrimaryContactNumber,
                    strEmailAddress = clsAccountUpdate.strEmailAddress,
                    strCompanyName = clsAccountUpdate.strCompanyName,
                    strVatNumber = clsAccountUpdate.strVatNumber,
                    strBusinessPurpose = clsAccountUpdate.strBusinessPurpose
                };


                if (clsAccountUpdate.strNewPassword != null && clsAccountUpdate.strNewPassword != "" && clsAccountUpdate.strConfirmNewPassword != null && clsAccountUpdate.strConfirmNewPassword != "")
                {
                    clsUsers.strPassword = clsCommonFunctions.GetMd5Sum(clsAccountUpdate.strNewPassword);
                    clsUsers.strPasswordConfirm = clsCommonFunctions.GetMd5Sum(clsAccountUpdate.strConfirmNewPassword);
                }
                else
                {
                    clsUsers.strPassword = clsAccountUpdate.strPassword;
                    clsUsers.strPasswordConfirm = clsAccountUpdate.strPasswordConfirm;
                }
                clsUsers.bIsConfirmed = clsAccountUpdate.bIsConfirmed;
                clsUsers.bIsDeleted = clsAccountUpdate.bIsDeleted;
                var clsUsersManager = new clsUsersManager();
                clsUsersManager.registerUser(clsUsers);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                //Reset error message
                ModelState.AddModelError("RegisterError", "Please fill out all of the required fields.");
            }
            return View(clsAccountUpdate);
        }
    }
}