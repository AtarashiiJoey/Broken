using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Colmart;
using Colmart.Models;

namespace Colmart.Model_Manager
{
    public class clsCMSUsersManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsCMSUsers> getAllCMSUsersList()
        {
            List<clsCMSUsers> lstCMSUsers = new List<clsCMSUsers>();
            var lstGetCMSUsersList = db.tblCMSUsers.Where(CMSUser => CMSUser.bIsDeleted == false).ToList();

            if (lstGetCMSUsersList.Count > 0)
            {
                clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager(); //CMS User Access Manager
                clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager(); //Role Type Manager

                foreach (var item in lstGetCMSUsersList)
                {
                    clsCMSUsers clsCMSUser = new clsCMSUsers();

                    clsCMSUser.iCMSUserID = item.iCMSUserID;
                    clsCMSUser.dtAdded = item.dtAdded;
                    clsCMSUser.iAddedBy = item.iAddedBy;
                    clsCMSUser.dtEdited = item.dtEdited;
                    clsCMSUser.iEditedBy = item.iEditedBy;

                    clsCMSUser.iCMSRoleTypeID = item.iCMSRoleTypeID;
                    clsCMSUser.strFirstName = item.strFirstName;
                    clsCMSUser.strSurname = item.strSurname;
                    clsCMSUser.strBiographicalInfo = item.strBiographicalInfo;
                    clsCMSUser.strContactNumber = item.strContactNumber;

                    clsCMSUser.strEmailAddress = item.strEmailAddress;
                    clsCMSUser.strPassword = item.strPassword;
                    clsCMSUser.strImagePath = item.strImagePath;
                    clsCMSUser.strImageName = item.strImageName;
                    clsCMSUser.bIsDeleted = item.bIsDeleted;

                    clsCMSUser.lstCMSUserAccess = new List<clsCMSUserAccess>();
                    if (item.tblCMSUserAccess.Count > 0)
                    {
                        foreach (var CMSUserAccessItem in item.tblCMSUserAccess)
                        {
                            clsCMSUserAccess clsCMSUserAccess = clsCMSUserAccessManager.convertCMSUserAccessTableToClass(CMSUserAccessItem);
                            clsCMSUser.lstCMSUserAccess.Add(clsCMSUserAccess);
                        }
                    }

                    if (item.tblCMSRoleTypes != null)
                        clsCMSUser.clsCMSRoleType = clsCMSRoleTypesManager.convertCMSRoleTypesTableToClass(item.tblCMSRoleTypes);

                    lstCMSUsers.Add(clsCMSUser);
                }
            }

            return lstCMSUsers;
        }

        //Get All
        public List<clsCMSUsers> getAllCMSUsersOnlyList()
        {
            List<clsCMSUsers> lstCMSUsers = new List<clsCMSUsers>();
            var lstGetCMSUsersList = db.tblCMSUsers.Where(CMSUser => CMSUser.bIsDeleted == false).ToList();

            if (lstGetCMSUsersList.Count > 0)
            {
                clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager(); //CMS User Access Manager
                clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager(); //Role Type Manager

                foreach (var item in lstGetCMSUsersList)
                {
                    clsCMSUsers clsCMSUser = new clsCMSUsers();

                    clsCMSUser.iCMSUserID = item.iCMSUserID;
                    clsCMSUser.dtAdded = item.dtAdded;
                    clsCMSUser.iAddedBy = item.iAddedBy;
                    clsCMSUser.dtEdited = item.dtEdited;
                    clsCMSUser.iEditedBy = item.iEditedBy;

                    clsCMSUser.iCMSRoleTypeID = item.iCMSRoleTypeID;
                    clsCMSUser.strFirstName = item.strFirstName;
                    clsCMSUser.strSurname = item.strSurname;
                    clsCMSUser.strBiographicalInfo = item.strBiographicalInfo;
                    clsCMSUser.strContactNumber = item.strContactNumber;

                    clsCMSUser.strEmailAddress = item.strEmailAddress;
                    clsCMSUser.strPassword = item.strPassword;
                    clsCMSUser.strImagePath = item.strImagePath;
                    clsCMSUser.strImageName = item.strImageName;
                    clsCMSUser.bIsDeleted = item.bIsDeleted;

                    clsCMSUser.lstCMSUserAccess = new List<clsCMSUserAccess>();

                    lstCMSUsers.Add(clsCMSUser);
                }
            }

            return lstCMSUsers;
        }

        //Get
        public clsCMSUsers getCMSUserById(int iCMSUserID)
        {
            clsCMSUsers clsCMSUser = null;
            tblCMSUsers tblCMSUser = db.tblCMSUsers.FirstOrDefault(cmsUser => cmsUser.iCMSUserID == iCMSUserID && cmsUser.bIsDeleted == false);

            if (tblCMSUser != null)
            {
                clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager(); //CMS User Access Manager
                clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager(); //Role Type Manager

                clsCMSUser = new clsCMSUsers();
                clsCMSUser.iCMSUserID = tblCMSUser.iCMSUserID;
                clsCMSUser.dtAdded = tblCMSUser.dtAdded;
                clsCMSUser.iAddedBy = tblCMSUser.iAddedBy;
                clsCMSUser.dtEdited = tblCMSUser.dtEdited;
                clsCMSUser.iEditedBy = tblCMSUser.iEditedBy;

                clsCMSUser.iCMSRoleTypeID = tblCMSUser.iCMSRoleTypeID;
                clsCMSUser.strFirstName = tblCMSUser.strFirstName;
                clsCMSUser.strSurname = tblCMSUser.strSurname;
                clsCMSUser.strBiographicalInfo = tblCMSUser.strBiographicalInfo;
                clsCMSUser.strContactNumber = tblCMSUser.strContactNumber;

                clsCMSUser.strEmailAddress = tblCMSUser.strEmailAddress;
                clsCMSUser.strPassword = tblCMSUser.strPassword;
                clsCMSUser.strImagePath = tblCMSUser.strImagePath;
                clsCMSUser.strImageName = tblCMSUser.strImageName;
                clsCMSUser.bIsDeleted = tblCMSUser.bIsDeleted;

                clsCMSUser.lstCMSUserAccess = new List<clsCMSUserAccess>();
                if (tblCMSUser.tblCMSUserAccess.Count > 0)
                {
                    foreach (var CMSUserAccessItem in tblCMSUser.tblCMSUserAccess)
                    {
                        clsCMSUserAccess clsCMSUserAccess = clsCMSUserAccessManager.convertCMSUserAccessTableToClass(CMSUserAccessItem);
                        clsCMSUser.lstCMSUserAccess.Add(clsCMSUserAccess);
                    }
                }

                if (tblCMSUser.tblCMSRoleTypes != null)
                    clsCMSUser.clsCMSRoleType = clsCMSRoleTypesManager.convertCMSRoleTypesTableToClass(tblCMSUser.tblCMSRoleTypes);
            }

            return clsCMSUser;
        }

        //Get CMS user by User email
        public clsCMSUsers getCMSUserByEmail(string strEmailAddress)
        {
            clsCMSUsers clsCMSUser = null;
            tblCMSUsers tblCMSUser = db.tblCMSUsers.FirstOrDefault(cmsUser => cmsUser.strEmailAddress == strEmailAddress && cmsUser.bIsDeleted == false);

            if (tblCMSUser != null)
            {
                clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager(); //CMS User Access Manager
                clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager(); //Role Type Manager

                clsCMSUser = new clsCMSUsers();
                clsCMSUser.iCMSUserID = tblCMSUser.iCMSUserID;
                clsCMSUser.dtAdded = tblCMSUser.dtAdded;
                clsCMSUser.iAddedBy = tblCMSUser.iAddedBy;
                clsCMSUser.dtEdited = tblCMSUser.dtEdited;
                clsCMSUser.iEditedBy = tblCMSUser.iEditedBy;

                clsCMSUser.iCMSRoleTypeID = tblCMSUser.iCMSRoleTypeID;
                clsCMSUser.strFirstName = tblCMSUser.strFirstName;
                clsCMSUser.strSurname = tblCMSUser.strSurname;
                clsCMSUser.strBiographicalInfo = tblCMSUser.strBiographicalInfo;
                clsCMSUser.strContactNumber = tblCMSUser.strContactNumber;

                clsCMSUser.strEmailAddress = tblCMSUser.strEmailAddress;
                clsCMSUser.strPassword = tblCMSUser.strPassword;
                clsCMSUser.strImagePath = tblCMSUser.strImagePath;
                clsCMSUser.strImageName = tblCMSUser.strImageName;
                clsCMSUser.bIsDeleted = tblCMSUser.bIsDeleted;

                clsCMSUser.lstCMSUserAccess = new List<clsCMSUserAccess>();
                if (tblCMSUser.tblCMSUserAccess.Count > 0)
                {
                    foreach (var CMSUserAccessItem in tblCMSUser.tblCMSUserAccess)
                    {
                        clsCMSUserAccess clsCMSUserAccess = clsCMSUserAccessManager.convertCMSUserAccessTableToClass(CMSUserAccessItem);
                        clsCMSUser.lstCMSUserAccess.Add(clsCMSUserAccess);
                    }
                }

                if (tblCMSUser.tblCMSRoleTypes != null)
                    clsCMSUser.clsCMSRoleType = clsCMSRoleTypesManager.convertCMSRoleTypesTableToClass(tblCMSUser.tblCMSRoleTypes);
            }

            return clsCMSUser;
        }

        //Save
        public void saveCMSUser(clsCMSUsers clsCMSUser)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsSessionCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblCMSUsers tblCMSUser = new tblCMSUsers();

                tblCMSUser.iCMSUserID = clsCMSUser.iCMSUserID;

                tblCMSUser.iCMSRoleTypeID = clsCMSUser.iCMSRoleTypeID;
                tblCMSUser.strFirstName = clsCMSUser.strFirstName;
                tblCMSUser.strSurname = clsCMSUser.strSurname;
                tblCMSUser.strBiographicalInfo = clsCMSUser.strBiographicalInfo;
                tblCMSUser.strContactNumber = clsCMSUser.strContactNumber;

                tblCMSUser.strEmailAddress = clsCMSUser.strEmailAddress;
                tblCMSUser.strPassword = clsCMSUser.strPassword;
                tblCMSUser.strImagePath = clsCMSUser.strImagePath;
                tblCMSUser.strImageName = clsCMSUser.strImageName;
                tblCMSUser.bIsDeleted = clsCMSUser.bIsDeleted;

                //Add
                if (tblCMSUser.iCMSUserID == 0)
                {
                    tblCMSUser.dtAdded = DateTime.Now;
                    tblCMSUser.iAddedBy = clsSessionCMSUser.iCMSUserID;
                    tblCMSUser.dtEdited = DateTime.Now;
                    tblCMSUser.iEditedBy = clsSessionCMSUser.iCMSUserID;

                    db.tblCMSUsers.Add(tblCMSUser);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblCMSUser.dtAdded = clsCMSUser.dtAdded;
                    tblCMSUser.iAddedBy = clsCMSUser.iAddedBy;
                    tblCMSUser.dtEdited = DateTime.Now;
                    tblCMSUser.iEditedBy = clsSessionCMSUser.iCMSUserID;

                    db.Set<tblCMSUsers>().AddOrUpdate(tblCMSUser);
                    db.SaveChanges();
                }
            }
        }

        //Reset Password
        public void resetCMSUserPassword(clsCMSUsers clsCMSUser)
        {
            clsCMSUsers clsSessionCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
            tblCMSUsers tblCMSUser = db.tblCMSUsers.Where(CMSUser => CMSUser.iCMSUserID == clsCMSUser.iCMSUserID && CMSUser.bIsDeleted == false).FirstOrDefault();

            if (tblCMSUser != null)
            {
                tblCMSUser.strPassword = clsCMSUser.strPassword;
                db.Set<tblCMSUsers>().AddOrUpdate(tblCMSUser);
                db.SaveChanges();
            }
        }

        //Remove CMS user by User ID (soft delete)
        public void removeCMSUserByID(int iCMSUserID)
        {
            tblCMSUsers tblCMSUser = db.tblCMSUsers.Find(iCMSUserID);
            if (tblCMSUser != null)
            {
                tblCMSUser.bIsDeleted = true;
                db.Entry(tblCMSUser).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check if CMS user Exists by ID
        public bool checkIfCMSUserExists(int iCMSUserID)
        {
            bool bCMSUserExists = db.tblCMSUsers.Any(cmsUser => cmsUser.iCMSUserID == iCMSUserID && cmsUser.bIsDeleted == false);
            return bCMSUserExists;
        }

        //Check if CMS user Exists by email
        public bool checkIfCMSUserExists(string strEmailAddress)
        {
            bool bCMSUserExists = db.tblCMSUsers.Any(cmsUser => cmsUser.strEmailAddress == strEmailAddress && cmsUser.bIsDeleted == false);
            return bCMSUserExists;
        }

        //Convert database table to class
        public clsCMSUsers convertCMSUsersTableToClass(tblCMSUsers tblCMSUser)
        {
            clsCMSUsers clsCMSUser = new clsCMSUsers();

            clsCMSUser.iCMSUserID = tblCMSUser.iCMSUserID;
            clsCMSUser.dtAdded = tblCMSUser.dtAdded;
            clsCMSUser.iAddedBy = tblCMSUser.iAddedBy;
            clsCMSUser.dtEdited = tblCMSUser.dtEdited;
            clsCMSUser.iEditedBy = tblCMSUser.iEditedBy;

            clsCMSUser.iCMSRoleTypeID = tblCMSUser.iCMSRoleTypeID;
            clsCMSUser.strFirstName = tblCMSUser.strFirstName;
            clsCMSUser.strSurname = tblCMSUser.strSurname;
            clsCMSUser.strBiographicalInfo = tblCMSUser.strBiographicalInfo;
            clsCMSUser.strContactNumber = tblCMSUser.strContactNumber;

            clsCMSUser.strEmailAddress = tblCMSUser.strEmailAddress;
            clsCMSUser.strPassword = tblCMSUser.strPassword;
            clsCMSUser.strImagePath = tblCMSUser.strImagePath;
            clsCMSUser.strImageName = tblCMSUser.strImageName;
            clsCMSUser.bIsDeleted = tblCMSUser.bIsDeleted;

            return clsCMSUser;
        }
    }
}
