using Colmart.Models;
using Colmart.View_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Colmart.Model_Manager
{
    public class clsUsersManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsUsers> getAllUsersList()
        {
            List<clsUsers> lstUsers = new List<clsUsers>();
            var lstGetUsersList = db.tblUsers.Where(User => User.bIsDeleted == false).ToList();

            if (lstGetUsersList.Count > 0)
            {
                clsUserAccessManager clsUserAccessManager = new clsUserAccessManager(); //User Access Manager
                clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager(); //Role Type Manager

                foreach (var item in lstGetUsersList)
                {
                    clsUsers clsUser = new clsUsers
                    {
                        iUserID = item.iUserID,
                        dtAdded = item.dtAdded,
                        iAddedBy = item.iAddedBy,
                        dtEdited = item.dtEdited,
                        iEditedBy = item.iEditedBy,
                        iRoleTypeID = item.iRoleTypeID,

                        strFirstName = item.strFirstName,
                        strSurname = item.strSurname,
                        strPrimaryContact = item.strPrimaryContact,
                        strSecondaryContact = item.strSecondaryContact,
                        strPrimaryContactNumber = item.strPrimaryContactNumber,
                        strSecondaryContactNumber = item.strSecondaryContactNumber,

                        strEmailAddress = item.strEmailAddress,
                        strCompanyName = item.strCompanyName,
                        strVatNumber = item.strVatNumber,
                        strBusinessPurpose = item.strBusinessPurpose,
                        strPassword = item.strPassword,
                        strPasswordConfirm = item.strPasswordConfirm,
                        strImagePath = item.strImagePath,
                        strImageName = item.strImageName,

                        bIsDeleted = item.bIsDeleted,
                        bIsConfirmed = item.bIsConfirmed,
                        lstUserAccess = new List<clsUserAccess>()
                    };




                    if (item.tblUserAccess.Count > 0)
                    {
                        foreach (var UserAccessItem in item.tblUserAccess)
                        {
                            clsUserAccess clsUserAccess = clsUserAccessManager.convertUserAccessTableToClass(UserAccessItem);
                            clsUser.lstUserAccess.Add(clsUserAccess);
                        }
                    }

                    if (item.tblRoleTypes != null)
                        clsUser.clsRoleType = clsRoleTypesManager.convertRoleTypesTableToClass(item.tblRoleTypes);

                    lstUsers.Add(clsUser);
                }
            }

            return lstUsers;
        }

        //Get All
        public List<clsUsers> getAllUsersOnlyList()
        {
            List<clsUsers> lstUsers = new List<clsUsers>();
            var lstGetUsersList = db.tblUsers.Where(User => User.bIsDeleted == false).ToList();

            if (lstGetUsersList.Count > 0)
            {
                clsUserAccessManager clsUserAccessManager = new clsUserAccessManager(); //User Access Manager
                clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager(); //Role Type Manager

                foreach (var item in lstGetUsersList)
                {
                    clsUsers clsUser = new clsUsers()
                    {
                        iUserID = item.iUserID,
                        dtAdded = item.dtAdded,
                        iAddedBy = item.iAddedBy,
                        dtEdited = item.dtEdited,
                        iEditedBy = item.iEditedBy,
                        iRoleTypeID = item.iRoleTypeID,

                        strFirstName = item.strFirstName,
                        strSurname = item.strSurname,
                        strPrimaryContact = item.strPrimaryContact,
                        strSecondaryContact = item.strSecondaryContact,
                        strPrimaryContactNumber = item.strPrimaryContactNumber,
                        strSecondaryContactNumber = item.strSecondaryContactNumber,

                        strEmailAddress = item.strEmailAddress,
                        strCompanyName = item.strCompanyName,
                        strVatNumber = item.strVatNumber,
                        strBusinessPurpose = item.strBusinessPurpose,
                        strPassword = item.strPassword,
                        strPasswordConfirm = item.strPasswordConfirm,
                        strImagePath = item.strImagePath,
                        strImageName = item.strImageName,

                        bIsDeleted = item.bIsDeleted,
                        bIsConfirmed = item.bIsConfirmed,

                        lstUserAccess = new List<clsUserAccess>()
                    };
                    lstUsers.Add(clsUser);
                }
            }

            return lstUsers;
        }

        //Get
        public clsUsers getUserById(int iUserID)
        {
            clsUsers clsUser = null;
            tblUsers tblUser = db.tblUsers.FirstOrDefault(user => user.iUserID == iUserID && user.bIsDeleted == false);

            if (tblUser != null)
            {
                clsUserAccessManager clsUserAccessManager = new clsUserAccessManager(); //User Access Manager
                clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager(); //Role Type Manager

                clsUser = new clsUsers
                {
                    iUserID = tblUser.iUserID,
                    dtAdded = tblUser.dtAdded,
                    iAddedBy = tblUser.iAddedBy,
                    dtEdited = tblUser.dtEdited,
                    iEditedBy = tblUser.iEditedBy,
                    iRoleTypeID = tblUser.iRoleTypeID,

                    strFirstName = tblUser.strFirstName,
                    strSurname = tblUser.strSurname,
                    strPrimaryContact = tblUser.strPrimaryContact,
                    strSecondaryContact = tblUser.strSecondaryContact,
                    strPrimaryContactNumber = tblUser.strPrimaryContactNumber,
                    strSecondaryContactNumber = tblUser.strSecondaryContactNumber,

                    strEmailAddress = tblUser.strEmailAddress,
                    strCompanyName = tblUser.strCompanyName,
                    strVatNumber = tblUser.strVatNumber,
                    strBusinessPurpose = tblUser.strBusinessPurpose,
                    strPassword = tblUser.strPassword,
                    strPasswordConfirm = tblUser.strPasswordConfirm,
                    strImagePath = tblUser.strImagePath,
                    strImageName = tblUser.strImageName,

                    bIsDeleted = tblUser.bIsDeleted,
                    bIsConfirmed = tblUser.bIsConfirmed,

                    lstUserAccess = new List<clsUserAccess>()
                };



                if (tblUser.tblUserAccess.Count > 0)
                {
                    foreach (var UserAccessItem in tblUser.tblUserAccess)
                    {
                        clsUserAccess clsUserAccess = clsUserAccessManager.convertUserAccessTableToClass(UserAccessItem);
                        clsUser.lstUserAccess.Add(clsUserAccess);
                    }
                }

                if (tblUser.tblRoleTypes != null)
                    clsUser.clsRoleType = clsRoleTypesManager.convertRoleTypesTableToClass(tblUser.tblRoleTypes);
            }

            return clsUser;
        }

        //Get user account by user ID
        public clsAccountUpdate getUserAccountById(int iUserID)
        {
            clsAccountUpdate clsUser = null;
            tblUsers tblUser = db.tblUsers.FirstOrDefault(user => user.iUserID == iUserID && user.bIsDeleted == false);

            if (tblUser != null)
            {
                clsUserAccessManager clsUserAccessManager = new clsUserAccessManager(); //User Access Manager
                clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager(); //Role Type Manager

                clsUser = new clsAccountUpdate
                {
                    iUserID = tblUser.iUserID,
                    dtAdded = tblUser.dtAdded,
                    iAddedBy = tblUser.iAddedBy,
                    dtEdited = tblUser.dtEdited,
                    iEditedBy = tblUser.iEditedBy,
                    iRoleTypeID = tblUser.iRoleTypeID,
                    strFirstName = tblUser.strFirstName,
                    strSurname = tblUser.strSurname,

                    strPrimaryContact = tblUser.strPrimaryContact,
                    strSecondaryContact = tblUser.strSecondaryContact,
                    strPrimaryContactNumber = tblUser.strPrimaryContactNumber,
                    strSecondaryContactNumber = tblUser.strSecondaryContactNumber,

                    strEmailAddress = tblUser.strEmailAddress,
                    strCompanyName = tblUser.strCompanyName,
                    strVatNumber = tblUser.strVatNumber,
                    strBusinessPurpose = tblUser.strBusinessPurpose,
                    strPassword = tblUser.strPassword,
                    strPasswordConfirm = tblUser.strPasswordConfirm,
                    strImagePath = tblUser.strImagePath,
                    strImageName = tblUser.strImageName,
                    bIsDeleted = tblUser.bIsDeleted,
                    bIsConfirmed = tblUser.bIsConfirmed
                };



            }

            return clsUser;
        }

        //Get CMS user by User email
        public clsUsers getUserByEmail(string strEmailAddress)
        {
            clsUsers clsUser = null;
            tblUsers tblUser = db.tblUsers.FirstOrDefault(user => user.strEmailAddress == strEmailAddress && user.bIsDeleted == false);

            if (tblUser != null)
            {
                clsUserAccessManager clsUserAccessManager = new clsUserAccessManager(); //User Access Manager
                clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager(); //Role Type Manager

                clsUser = new clsUsers
                {
                    iUserID = tblUser.iUserID,
                    dtAdded = tblUser.dtAdded,
                    iAddedBy = tblUser.iAddedBy,
                    dtEdited = tblUser.dtEdited,
                    iEditedBy = tblUser.iEditedBy,
                    iRoleTypeID = tblUser.iRoleTypeID,
                    strFirstName = tblUser.strFirstName,
                    strSurname = tblUser.strSurname,

                    strPrimaryContact = tblUser.strPrimaryContact,
                    strSecondaryContact = tblUser.strSecondaryContact,
                    strPrimaryContactNumber = tblUser.strPrimaryContactNumber,
                    strSecondaryContactNumber = tblUser.strSecondaryContactNumber,

                    strEmailAddress = tblUser.strEmailAddress,
                    strCompanyName = tblUser.strCompanyName,
                    strVatNumber = tblUser.strVatNumber,
                    strBusinessPurpose = tblUser.strBusinessPurpose,
                    strPassword = tblUser.strPassword,
                    strPasswordConfirm = tblUser.strPasswordConfirm,
                    strImagePath = tblUser.strImagePath,
                    strImageName = tblUser.strImageName,
                    bIsDeleted = tblUser.bIsDeleted,
                    bIsConfirmed = tblUser.bIsConfirmed,

                    lstUserAccess = new List<clsUserAccess>()
                };


                if (tblUser.tblUserAccess.Count > 0)
                {
                    foreach (var UserAccessItem in tblUser.tblUserAccess)
                    {
                        clsUserAccess clsUserAccess = clsUserAccessManager.convertUserAccessTableToClass(UserAccessItem);
                        clsUser.lstUserAccess.Add(clsUserAccess);
                    }
                }

                if (tblUser.tblRoleTypes != null)
                    clsUser.clsRoleType = clsRoleTypesManager.convertRoleTypesTableToClass(tblUser.tblRoleTypes);
            }

            return clsUser;
        }

        //Save
        public void saveUser(clsUsers clsUser)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsSessionCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblUsers tblUser = new tblUsers
                {
                    iUserID = clsUser.iUserID,
                    iRoleTypeID = clsUser.iRoleTypeID,
                    strFirstName = clsUser.strFirstName,
                    strSurname = clsUser.strSurname,

                    strEmailAddress = clsUser.strEmailAddress,
                    strCompanyName = clsUser.strCompanyName,
                    strVatNumber = clsUser.strVatNumber,
                    strBusinessPurpose = clsUser.strBusinessPurpose,
                    strPassword = clsUser.strPassword,
                    strPasswordConfirm = clsUser.strPasswordConfirm,
                    strImagePath = clsUser.strImagePath,
                    strImageName = clsUser.strImageName,
                    bIsDeleted = clsUser.bIsDeleted,
                    bIsConfirmed = clsUser.bIsConfirmed,
                    strPrimaryContact = clsUser.strPrimaryContact,
                    strSecondaryContact = clsUser.strSecondaryContact,
                    strPrimaryContactNumber = clsUser.strPrimaryContactNumber,
                    strSecondaryContactNumber = clsUser.strSecondaryContactNumber
                };




                //Add
                if (tblUser.iUserID == 0)
                {
                    tblUser.dtAdded = DateTime.Now;
                    tblUser.iAddedBy = clsSessionCMSUser.iCMSUserID;
                    tblUser.dtEdited = DateTime.Now;
                    tblUser.iEditedBy = clsSessionCMSUser.iCMSUserID;

                    db.tblUsers.Add(tblUser);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblUser.dtAdded = clsUser.dtAdded;
                    tblUser.iAddedBy = clsUser.iAddedBy;
                    tblUser.dtEdited = DateTime.Now;
                    tblUser.iEditedBy = clsSessionCMSUser.iCMSUserID;

                    db.Set<tblUsers>().AddOrUpdate(tblUser);
                    db.SaveChanges();
                }
            }
        }

        //Save
        public int registerUser(clsUsers clsUser)
        {
            tblUsers tblUser = new tblUsers
            {

                iUserID = clsUser.iUserID,
                iRoleTypeID = clsUser.iRoleTypeID,
                strFirstName = clsUser.strFirstName,
                strSurname = clsUser.strSurname,

                strEmailAddress = clsUser.strEmailAddress,
                strCompanyName = clsUser.strCompanyName,
                strVatNumber = clsUser.strVatNumber,
                strBusinessPurpose = clsUser.strBusinessPurpose,
                strPassword = clsUser.strPassword,
                strPasswordConfirm = clsUser.strPasswordConfirm,
                strImagePath = clsUser.strImagePath,
                strImageName = clsUser.strImageName,
                bIsDeleted = clsUser.bIsDeleted,
                bIsConfirmed = clsUser.bIsConfirmed,
                strPrimaryContact = clsUser.strPrimaryContact,
                strSecondaryContact = clsUser.strSecondaryContact,
                strPrimaryContactNumber = clsUser.strPrimaryContactNumber,
                strSecondaryContactNumber = clsUser.strSecondaryContactNumber
            };

            //Add
            if (tblUser.iUserID == 0)
            {
                tblUser.dtAdded = DateTime.Now;
                tblUser.iAddedBy = 1;
                tblUser.dtEdited = DateTime.Now;
                tblUser.iEditedBy = 1;

                db.tblUsers.Add(tblUser);
                db.SaveChanges();
            }
            //Update
            else
            {
                tblUser.dtAdded = clsUser.dtAdded;
                tblUser.iAddedBy = clsUser.iAddedBy;
                tblUser.dtEdited = DateTime.Now;
                tblUser.iEditedBy = 1;

                db.Set<tblUsers>().AddOrUpdate(tblUser);
                db.SaveChanges();
            }
            return tblUser.iUserID;
        }

        //Remove user by User ID (soft delete)
        public void removeUserByID(int iUserID)
        {
            tblUsers tblUser = db.tblUsers.Find(iUserID);
            if (tblUser != null)
            {
                tblUser.bIsDeleted = true;
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check if user Exists by ID
        public bool checkIfUserExists(int iUserID)
        {
            bool bUserExists = db.tblUsers.Any(User => User.iUserID == iUserID && User.bIsDeleted == false);
            return bUserExists;
        }

        //Check if  user Exists by email
        public bool checkIfUserExists(string strEmailAddress)
        {
            bool bUserExists = db.tblUsers.Any(User => User.strEmailAddress == strEmailAddress && User.bIsDeleted == false);
            return bUserExists;
        }

        //Convert database table to class
        public clsUsers convertUsersTableToClass(tblUsers tblUser)
        {
            clsUsers clsUser = new clsUsers
            {
                iUserID = tblUser.iUserID,
                dtAdded = tblUser.dtAdded,
                iAddedBy = tblUser.iAddedBy,
                dtEdited = tblUser.dtEdited,
                iEditedBy = tblUser.iEditedBy,
                iRoleTypeID = tblUser.iRoleTypeID,
                strFirstName = tblUser.strFirstName,
                strSurname = tblUser.strSurname,

                strPrimaryContact = tblUser.strPrimaryContact,
                strSecondaryContact = tblUser.strSecondaryContact,
                strPrimaryContactNumber = tblUser.strPrimaryContactNumber,
                strSecondaryContactNumber = tblUser.strSecondaryContactNumber,

                strEmailAddress = tblUser.strEmailAddress,
                strCompanyName = tblUser.strCompanyName,
                strVatNumber = tblUser.strVatNumber,
                strBusinessPurpose = tblUser.strBusinessPurpose,
                strPassword = tblUser.strPassword,
                strPasswordConfirm = tblUser.strPasswordConfirm,
                strImagePath = tblUser.strImagePath,
                strImageName = tblUser.strImageName,
                bIsDeleted = tblUser.bIsDeleted,
                bIsConfirmed = tblUser.bIsConfirmed
            };
            return clsUser;
        }

        //Reset Password
        public void resetUserPassword(clsUsers clsUser)
        {
            clsUsers clsSessionUser = (clsUsers)HttpContext.Current.Session["clsUser"];
            tblUsers tblUser = db.tblUsers.FirstOrDefault(User => User.iUserID == clsUser.iUserID && User.bIsDeleted == false);

            if (tblUser != null)
            {
                tblUser.strPassword = clsUser.strPassword;
                db.Set<tblUsers>().AddOrUpdate(tblUser);
                db.SaveChanges();
            }
        }
    }
}
