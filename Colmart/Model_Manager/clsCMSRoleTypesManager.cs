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
    public class clsCMSRoleTypesManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsCMSRoleTypes> getAllCMSRoleTypesList()
        {
            List<clsCMSRoleTypes> lstCMSRoleTypes = new List<clsCMSRoleTypes>();
            var lstGetCMSRoleTypesList = db.tblCMSRoleTypes.Where(CMSRoleType => CMSRoleType.bIsDeleted == false).ToList();

            if (lstGetCMSRoleTypesList.Count > 0)
            {
                clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager(); //CMS User Manager
                clsUsersManager clsUsersManager = new clsUsersManager(); //User Manager

                foreach (var item in lstGetCMSRoleTypesList)
                {
                    clsCMSRoleTypes clsCMSRoleType = new clsCMSRoleTypes();

                    clsCMSRoleType.iCMSRoleTypeID = item.iCMSRoleTypeID;
                    clsCMSRoleType.dtAdded = item.dtAdded;
                    clsCMSRoleType.iAddedBy = item.iAddedBy;
                    clsCMSRoleType.dtEdited = item.dtEdited;
                    clsCMSRoleType.iEditedBy = item.iEditedBy;

                    clsCMSRoleType.strTitle = item.strTitle;
                    clsCMSRoleType.bIsDeleted = item.bIsDeleted;

                    clsCMSRoleType.lstCMSUsers = new List<clsCMSUsers>();

                    if (item.tblCMSUsers.Count > 0)
                    {
                        foreach (var CMSUserItem in item.tblCMSUsers)
                        {
                            clsCMSUsers clsCMSUser = clsCMSUsersManager.convertCMSUsersTableToClass(CMSUserItem);
                            clsCMSRoleType.lstCMSUsers.Add(clsCMSUser);
                        }
                    }

                    lstCMSRoleTypes.Add(clsCMSRoleType);
                }
            }

            return lstCMSRoleTypes;
        }

        //Get All
        public List<clsCMSRoleTypes> getAllCMSRoleTypesOnlyList()
        {
            List<clsCMSRoleTypes> lstCMSRoleTypes = new List<clsCMSRoleTypes>();
            var lstGetCMSRoleTypesList = db.tblCMSRoleTypes.Where(CMSRoleType => CMSRoleType.bIsDeleted == false).ToList();

            if (lstGetCMSRoleTypesList.Count > 0)
            {
                clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager(); //CMS User Manager
                clsUsersManager clsUsersManager = new clsUsersManager(); //User Manager

                foreach (var item in lstGetCMSRoleTypesList)
                {
                    clsCMSRoleTypes clsCMSRoleType = new clsCMSRoleTypes();

                    clsCMSRoleType.iCMSRoleTypeID = item.iCMSRoleTypeID;
                    clsCMSRoleType.dtAdded = item.dtAdded;
                    clsCMSRoleType.iAddedBy = item.iAddedBy;
                    clsCMSRoleType.dtEdited = item.dtEdited;
                    clsCMSRoleType.iEditedBy = item.iEditedBy;

                    clsCMSRoleType.strTitle = item.strTitle;
                    clsCMSRoleType.bIsDeleted = item.bIsDeleted;

                    clsCMSRoleType.lstCMSUsers = new List<clsCMSUsers>();

                    lstCMSRoleTypes.Add(clsCMSRoleType);
                }
            }

            return lstCMSRoleTypes;
        }

        //Get
        public clsCMSRoleTypes getCMSRoleTypeByID(int iCMSRoleTypeID)
        {
            clsCMSRoleTypes clsCMSRoleType = null;
            tblCMSRoleTypes tblCMSRoleType = db.tblCMSRoleTypes.FirstOrDefault(CMSRoleType => CMSRoleType.iCMSRoleTypeID == iCMSRoleTypeID && CMSRoleType.bIsDeleted == false);

            if (tblCMSRoleType != null)
            {
                clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager(); //CMS User Manager
                clsUsersManager clsUsersManager = new clsUsersManager(); //User Manager

                clsCMSRoleType = new clsCMSRoleTypes();
                clsCMSRoleType.iCMSRoleTypeID = tblCMSRoleType.iCMSRoleTypeID;
                clsCMSRoleType.dtAdded = tblCMSRoleType.dtAdded;
                clsCMSRoleType.iAddedBy = tblCMSRoleType.iAddedBy;
                clsCMSRoleType.dtEdited = tblCMSRoleType.dtEdited;
                clsCMSRoleType.iEditedBy = tblCMSRoleType.iEditedBy;

                clsCMSRoleType.strTitle = tblCMSRoleType.strTitle;
                clsCMSRoleType.bIsDeleted = tblCMSRoleType.bIsDeleted;

                clsCMSRoleType.lstCMSUsers = new List<clsCMSUsers>();

                if (tblCMSRoleType.tblCMSUsers.Count > 0)
                {
                    foreach (var CMSUserItem in tblCMSRoleType.tblCMSUsers)
                    {
                        clsCMSUsers clsCMSUser = clsCMSUsersManager.convertCMSUsersTableToClass(CMSUserItem);
                        clsCMSRoleType.lstCMSUsers.Add(clsCMSUser);
                    }
                }
            }

            return clsCMSRoleType;
        }

        //Save
        public void saveCMSRoleType(clsCMSRoleTypes clsCMSRoleType)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblCMSRoleTypes tblCMSRoleType = new tblCMSRoleTypes();

                tblCMSRoleType.iCMSRoleTypeID = clsCMSRoleType.iCMSRoleTypeID;

                tblCMSRoleType.strTitle = clsCMSRoleType.strTitle;
                tblCMSRoleType.bIsDeleted = clsCMSRoleType.bIsDeleted;

                //Add
                if (tblCMSRoleType.iCMSRoleTypeID == 0)
                {
                    tblCMSRoleType.dtAdded = DateTime.Now;
                    tblCMSRoleType.iAddedBy = clsCMSUser.iCMSUserID;
                    tblCMSRoleType.dtEdited = DateTime.Now;
                    tblCMSRoleType.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblCMSRoleTypes.Add(tblCMSRoleType);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblCMSRoleType.dtAdded = clsCMSRoleType.dtAdded;
                    tblCMSRoleType.iAddedBy = clsCMSRoleType.iAddedBy;
                    tblCMSRoleType.dtEdited = DateTime.Now;
                    tblCMSRoleType.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblCMSRoleTypes>().AddOrUpdate(tblCMSRoleType);
                    db.SaveChanges();
                }
            }
        }

        //Remove
        public void removeCMSRoleTypeByID(int iCMSRoleTypeID)
        {
            tblCMSRoleTypes tblCMSRoleType = db.tblCMSRoleTypes.Find(iCMSRoleTypeID);
            if (tblCMSRoleType != null)
            {
                tblCMSRoleType.bIsDeleted = true;
                db.Entry(tblCMSRoleType).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check
        public bool checkIfCMSRoleTypeExists(int iCMSRoleTypeID)
        {
            bool bCMSRoleTypeExists = db.tblCMSRoleTypes.Any(CMSRoleType => CMSRoleType.iCMSRoleTypeID == iCMSRoleTypeID && CMSRoleType.bIsDeleted == false);
            return bCMSRoleTypeExists;
        }

        //Convert database table to class
        public clsCMSRoleTypes convertCMSRoleTypesTableToClass(tblCMSRoleTypes tblCMSRoleType)
        {
            clsCMSRoleTypes clsCMSRoleType = new clsCMSRoleTypes();

            clsCMSRoleType.iCMSRoleTypeID = tblCMSRoleType.iCMSRoleTypeID;
            clsCMSRoleType.dtAdded = tblCMSRoleType.dtAdded;
            clsCMSRoleType.iAddedBy = tblCMSRoleType.iAddedBy;
            clsCMSRoleType.dtEdited = tblCMSRoleType.dtEdited;
            clsCMSRoleType.iEditedBy = tblCMSRoleType.iEditedBy;

            clsCMSRoleType.strTitle = tblCMSRoleType.strTitle;
            clsCMSRoleType.bIsDeleted = tblCMSRoleType.bIsDeleted;

            return clsCMSRoleType;
        }
    }
}
