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
                    clsUsers clsUser = new clsUsers();

                    clsUser.iUserID = item.iUserID;
                    clsUser.dtAdded = item.dtAdded;
                    clsUser.iAddedBy = item.iAddedBy;
                    clsUser.dtEdited = item.dtEdited;
                    clsUser.iEditedBy = item.iEditedBy;

                    clsUser.iRoleTypeID = item.iRoleTypeID;
                    clsUser.strFirstName = item.strFirstName;
                    clsUser.strSurname = item.strSurname;
                    clsUser.strBiographicalInfo = item.strBiographicalInfo;
                    clsUser.strContactNumber = item.strContactNumber;

                    clsUser.strEmailAddress = item.strEmailAddress;
                    clsUser.strPassword = item.strPassword;
                    clsUser.strImagePath = item.strImagePath;
                    clsUser.strImageName = item.strImageName;
                    clsUser.bIsDeleted = item.bIsDeleted;

                    clsUser.lstUserAccess = new List<clsUserAccess>();
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
                    clsUsers clsUser = new clsUsers();

                    clsUser.iUserID = item.iUserID;
                    clsUser.dtAdded = item.dtAdded;
                    clsUser.iAddedBy = item.iAddedBy;
                    clsUser.dtEdited = item.dtEdited;
                    clsUser.iEditedBy = item.iEditedBy;

                    clsUser.iRoleTypeID = item.iRoleTypeID;
                    clsUser.strFirstName = item.strFirstName;
                    clsUser.strSurname = item.strSurname;
                    clsUser.strBiographicalInfo = item.strBiographicalInfo;
                    clsUser.strContactNumber = item.strContactNumber;

                    clsUser.strEmailAddress = item.strEmailAddress;
                    clsUser.strPassword = item.strPassword;
                    clsUser.strImagePath = item.strImagePath;
                    clsUser.strImageName = item.strImageName;
                    clsUser.bIsDeleted = item.bIsDeleted;

                    clsUser.lstUserAccess = new List<clsUserAccess>();

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

                clsUser = new clsUsers();
                clsUser.iUserID = tblUser.iUserID;
                clsUser.dtAdded = tblUser.dtAdded;
                clsUser.iAddedBy = tblUser.iAddedBy;
                clsUser.dtEdited = tblUser.dtEdited;
                clsUser.iEditedBy = tblUser.iEditedBy;

                clsUser.iRoleTypeID = tblUser.iRoleTypeID;
                clsUser.strFirstName = tblUser.strFirstName;
                clsUser.strSurname = tblUser.strSurname;
                clsUser.strBiographicalInfo = tblUser.strBiographicalInfo;
                clsUser.strContactNumber = tblUser.strContactNumber;

                clsUser.strEmailAddress = tblUser.strEmailAddress;
                clsUser.strPassword = tblUser.strPassword;
                clsUser.strImagePath = tblUser.strImagePath;
                clsUser.strImageName = tblUser.strImageName;
                clsUser.bIsDeleted = tblUser.bIsDeleted;

                clsUser.lstUserAccess = new List<clsUserAccess>();
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

        //Get CMS user by User email
        public clsUsers getUserByEmail(string strEmailAddress)
        {
            clsUsers clsUser = null;
            tblUsers tblUser = db.tblUsers.FirstOrDefault(user => user.strEmailAddress == strEmailAddress && user.bIsDeleted == false);

            if (tblUser != null)
            {
                clsUserAccessManager clsUserAccessManager = new clsUserAccessManager(); //User Access Manager
                clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager(); //Role Type Manager

                clsUser = new clsUsers();
                clsUser.iUserID = tblUser.iUserID;
                clsUser.dtAdded = tblUser.dtAdded;
                clsUser.iAddedBy = tblUser.iAddedBy;
                clsUser.dtEdited = tblUser.dtEdited;
                clsUser.iEditedBy = tblUser.iEditedBy;

                clsUser.iRoleTypeID = tblUser.iRoleTypeID;
                clsUser.strFirstName = tblUser.strFirstName;
                clsUser.strSurname = tblUser.strSurname;
                clsUser.strBiographicalInfo = tblUser.strBiographicalInfo;
                clsUser.strContactNumber = tblUser.strContactNumber;

                clsUser.strEmailAddress = tblUser.strEmailAddress;
                clsUser.strPassword = tblUser.strPassword;
                clsUser.strImagePath = tblUser.strImagePath;
                clsUser.strImageName = tblUser.strImageName;
                clsUser.bIsDeleted = tblUser.bIsDeleted;

                clsUser.lstUserAccess = new List<clsUserAccess>();
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
                tblUsers tblUser = new tblUsers();

                tblUser.iUserID = clsUser.iUserID;

                tblUser.iRoleTypeID = clsUser.iRoleTypeID;
                tblUser.strFirstName = clsUser.strFirstName;
                tblUser.strSurname = clsUser.strSurname;
                tblUser.strBiographicalInfo = clsUser.strBiographicalInfo;
                tblUser.strContactNumber = clsUser.strContactNumber;

                tblUser.strEmailAddress = clsUser.strEmailAddress;
                tblUser.strPassword = clsUser.strPassword;
                tblUser.strImagePath = clsUser.strImagePath;
                tblUser.strImageName = clsUser.strImageName;
                tblUser.bIsDeleted = clsUser.bIsDeleted;

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
            clsUsers clsUser = new clsUsers();

            clsUser.iUserID = tblUser.iUserID;
            clsUser.dtAdded = tblUser.dtAdded;
            clsUser.iAddedBy = tblUser.iAddedBy;
            clsUser.dtEdited = tblUser.dtEdited;
            clsUser.iEditedBy = tblUser.iEditedBy;

            clsUser.iRoleTypeID = tblUser.iRoleTypeID;
            clsUser.strFirstName = tblUser.strFirstName;
            clsUser.strSurname = tblUser.strSurname;
            clsUser.strBiographicalInfo = tblUser.strBiographicalInfo;
            clsUser.strContactNumber = tblUser.strContactNumber;

            clsUser.strEmailAddress = tblUser.strEmailAddress;
            clsUser.strPassword = tblUser.strPassword;
            clsUser.strImagePath = tblUser.strImagePath;
            clsUser.strImageName = tblUser.strImageName;
            clsUser.bIsDeleted = tblUser.bIsDeleted;

            return clsUser;
        }
    }
}
