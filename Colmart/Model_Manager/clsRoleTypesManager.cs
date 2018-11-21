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
    public class clsRoleTypesManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsRoleTypes> getAllRoleTypesList()
        {
            List<clsRoleTypes> lstRoleTypes = new List<clsRoleTypes>();
            var lstGetRoleTypesList = db.tblRoleTypes.Where(RoleType => RoleType.bIsDeleted == false).ToList();

            if (lstGetRoleTypesList.Count > 0)
            {
                clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager(); //CMS User Manager
                clsUsersManager clsUsersManager = new clsUsersManager(); //User Manager

                foreach (var item in lstGetRoleTypesList)
                {
                    clsRoleTypes clsRoleType = new clsRoleTypes();

                    clsRoleType.iRoleTypeID = item.iRoleTypeID;
                    clsRoleType.dtAdded = item.dtAdded;
                    clsRoleType.iAddedBy = item.iAddedBy;
                    clsRoleType.dtEdited = item.dtEdited;
                    clsRoleType.iEditedBy = item.iEditedBy;

                    clsRoleType.strTitle = item.strTitle;
                    clsRoleType.bIsDeleted = item.bIsDeleted;

                    clsRoleType.lstUsers = new List<clsUsers>();

                    if (item.tblUsers.Count > 0)
                    {
                        foreach (var UserItem in item.tblUsers)
                        {
                            clsUsers clsUser = clsUsersManager.convertUsersTableToClass(UserItem);
                            clsRoleType.lstUsers.Add(clsUser);
                        }
                    }

                    lstRoleTypes.Add(clsRoleType);
                }
            }

            return lstRoleTypes;
        }

        //Get All
        public List<clsRoleTypes> getAllRoleTypesOnlyList()
        {
            List<clsRoleTypes> lstRoleTypes = new List<clsRoleTypes>();
            var lstGetRoleTypesList = db.tblRoleTypes.Where(RoleType => RoleType.bIsDeleted == false).ToList();

            if (lstGetRoleTypesList.Count > 0)
            {
                clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager(); //CMS User Manager
                clsUsersManager clsUsersManager = new clsUsersManager(); //User Manager

                foreach (var item in lstGetRoleTypesList)
                {
                    clsRoleTypes clsRoleType = new clsRoleTypes();

                    clsRoleType.iRoleTypeID = item.iRoleTypeID;
                    clsRoleType.dtAdded = item.dtAdded;
                    clsRoleType.iAddedBy = item.iAddedBy;
                    clsRoleType.dtEdited = item.dtEdited;
                    clsRoleType.iEditedBy = item.iEditedBy;

                    clsRoleType.strTitle = item.strTitle;
                    clsRoleType.bIsDeleted = item.bIsDeleted;

                    clsRoleType.lstUsers = new List<clsUsers>();

                    lstRoleTypes.Add(clsRoleType);
                }
            }

            return lstRoleTypes;
        }

        //Get
        public clsRoleTypes getRoleTypeByID(int iRoleTypeID)
        {
            clsRoleTypes clsRoleType = null;
            tblRoleTypes tblRoleType = db.tblRoleTypes.FirstOrDefault(roleType => roleType.iRoleTypeID == iRoleTypeID && roleType.bIsDeleted == false);

            if (tblRoleType != null)
            {
                clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager(); //CMS User Manager
                clsUsersManager clsUsersManager = new clsUsersManager(); //User Manager

                clsRoleType = new clsRoleTypes();
                clsRoleType.iRoleTypeID = tblRoleType.iRoleTypeID;
                clsRoleType.dtAdded = tblRoleType.dtAdded;
                clsRoleType.iAddedBy = tblRoleType.iAddedBy;
                clsRoleType.dtEdited = tblRoleType.dtEdited;
                clsRoleType.iEditedBy = tblRoleType.iEditedBy;

                clsRoleType.strTitle = tblRoleType.strTitle;
                clsRoleType.bIsDeleted = tblRoleType.bIsDeleted;
                clsRoleType.lstUsers = new List<clsUsers>();

                if (tblRoleType.tblUsers.Count > 0)
                {
                    foreach (var UserItem in tblRoleType.tblUsers)
                    {
                        clsUsers clsUser = clsUsersManager.convertUsersTableToClass(UserItem);
                        clsRoleType.lstUsers.Add(clsUser);
                    }
                }
            }

            return clsRoleType;
        }

        //Save
        public void saveRoleType(clsRoleTypes clsRoleType)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblRoleTypes tblRoleType = new tblRoleTypes();

                tblRoleType.iRoleTypeID = clsRoleType.iRoleTypeID;

                tblRoleType.strTitle = clsRoleType.strTitle;
                tblRoleType.bIsDeleted = clsRoleType.bIsDeleted;

                //Add
                if (tblRoleType.iRoleTypeID == 0)
                {
                    tblRoleType.dtAdded = DateTime.Now;
                    tblRoleType.iAddedBy = clsCMSUser.iCMSUserID;
                    tblRoleType.dtEdited = DateTime.Now;
                    tblRoleType.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblRoleTypes.Add(tblRoleType);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblRoleType.dtAdded = clsRoleType.dtAdded;
                    tblRoleType.iAddedBy = clsRoleType.iAddedBy;
                    tblRoleType.dtEdited = DateTime.Now;
                    tblRoleType.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblRoleTypes>().AddOrUpdate(tblRoleType);
                    db.SaveChanges();
                }
            }
        }

        //Remove
        public void removeRoleTypeByID(int iRoleTypeID)
        {
            tblRoleTypes tblRoleType = db.tblRoleTypes.Find(iRoleTypeID);
            if (tblRoleType != null)
            {
                tblRoleType.bIsDeleted = true;
                db.Entry(tblRoleType).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check
        public bool checkIfRoleTypeExists(int iRoleTypeID)
        {
            bool bRoleTypeExists = db.tblRoleTypes.Any(RoleType => RoleType.iRoleTypeID == iRoleTypeID && RoleType.bIsDeleted == false);
            return bRoleTypeExists;
        }

        //Convert database table to class
        public clsRoleTypes convertRoleTypesTableToClass(tblRoleTypes tblRoleType)
        {
            clsRoleTypes clsRoleType = new clsRoleTypes();

            clsRoleType.iRoleTypeID = tblRoleType.iRoleTypeID;
            clsRoleType.dtAdded = tblRoleType.dtAdded;
            clsRoleType.iAddedBy = tblRoleType.iAddedBy;
            clsRoleType.dtEdited = tblRoleType.dtEdited;
            clsRoleType.iEditedBy = tblRoleType.iEditedBy;

            clsRoleType.strTitle = tblRoleType.strTitle;
            clsRoleType.bIsDeleted = tblRoleType.bIsDeleted;

            return clsRoleType;
        }
    }
}
