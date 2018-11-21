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
    public class clsUserAccessManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsUserAccess> getAllUserAccessList()
        {
            List<clsUserAccess> lstUserAccess = new List<clsUserAccess>();
            var lstGetUserAccessList = db.tblUserAccess.Where(UserAccess => UserAccess.bIsDeleted == false).ToList();

            if (lstGetUserAccessList.Count > 0)
            {
                clsUsersManager clsUsersManager = new clsUsersManager(); //User Manager
                clsPagesManager clsPagesManager = new clsPagesManager(); //Pages Manger

                foreach (var item in lstGetUserAccessList)
                {
                    clsUserAccess clsUserAccess = new clsUserAccess();

                    clsUserAccess.iUserAccessID = item.iUserAccessID;
                    clsUserAccess.dtAdded = item.dtAdded;
                    clsUserAccess.iAddedBy = item.iAddedBy;
                    clsUserAccess.dtEdited = item.dtEdited;
                    clsUserAccess.iEditedBy = item.iEditedBy;

                    clsUserAccess.iUserID = item.iUserID;
                    clsUserAccess.iPageID = item.iPageID;
                    clsUserAccess.bIsRead = item.bIsRead;
                    clsUserAccess.bIsWrite = item.bIsWrite;
                    clsUserAccess.bIsDeleted = item.bIsDeleted;

                    if (item.tblUsers != null)
                        clsUserAccess.clsUser = clsUsersManager.convertUsersTableToClass(item.tblUsers);
                    if (item.tblPages != null)
                        clsUserAccess.clsPage = clsPagesManager.convertPagesTableToClass(item.tblPages);

                    lstUserAccess.Add(clsUserAccess);
                }
            }

            return lstUserAccess;
        }

        //Get All
        public List<clsUserAccess> getAllUserAccessOnlyList()
        {
            List<clsUserAccess> lstUserAccess = new List<clsUserAccess>();
            var lstGetUserAccessList = db.tblUserAccess.Where(UserAccess => UserAccess.bIsDeleted == false).ToList();

            if (lstGetUserAccessList.Count > 0)
            {
                clsUsersManager clsUsersManager = new clsUsersManager(); //User Manager
                clsPagesManager clsPagesManager = new clsPagesManager(); //Pages Manger

                foreach (var item in lstGetUserAccessList)
                {
                    clsUserAccess clsUserAccess = new clsUserAccess();

                    clsUserAccess.iUserAccessID = item.iUserAccessID;
                    clsUserAccess.dtAdded = item.dtAdded;
                    clsUserAccess.iAddedBy = item.iAddedBy;
                    clsUserAccess.dtEdited = item.dtEdited;
                    clsUserAccess.iEditedBy = item.iEditedBy;

                    clsUserAccess.iUserID = item.iUserID;
                    clsUserAccess.iPageID = item.iPageID;
                    clsUserAccess.bIsRead = item.bIsRead;
                    clsUserAccess.bIsWrite = item.bIsWrite;
                    clsUserAccess.bIsDeleted = item.bIsDeleted;

                    lstUserAccess.Add(clsUserAccess);
                }
            }

            return lstUserAccess;
        }

        //Get
        public clsUserAccess getUserAccessByID(int iUserAccessID)
        {
            clsUserAccess clsUserAccess = null;
            tblUserAccess tblUserAccess = db.tblUserAccess.FirstOrDefault(UserAccess => UserAccess.iUserAccessID == iUserAccessID && UserAccess.bIsDeleted == false);

            if (tblUserAccess != null)
            {
                clsUsersManager clsUsersManager = new clsUsersManager(); //User Manager
                clsPagesManager clsPagesManager = new clsPagesManager(); //Pages Manger

                clsUserAccess = new clsUserAccess();

                clsUserAccess.iUserAccessID = tblUserAccess.iUserAccessID;
                clsUserAccess.dtAdded = tblUserAccess.dtAdded;
                clsUserAccess.iAddedBy = tblUserAccess.iAddedBy;
                clsUserAccess.dtEdited = tblUserAccess.dtEdited;
                clsUserAccess.iEditedBy = tblUserAccess.iEditedBy;

                clsUserAccess.iUserID = tblUserAccess.iUserID;
                clsUserAccess.iPageID = tblUserAccess.iPageID;
                clsUserAccess.bIsRead = tblUserAccess.bIsRead;
                clsUserAccess.bIsWrite = tblUserAccess.bIsWrite;
                clsUserAccess.bIsDeleted = tblUserAccess.bIsDeleted;

                if (tblUserAccess.tblUsers != null)
                    clsUserAccess.clsUser = clsUsersManager.convertUsersTableToClass(tblUserAccess.tblUsers);
                if (tblUserAccess.tblPages != null)
                    clsUserAccess.clsPage = clsPagesManager.convertPagesTableToClass(tblUserAccess.tblPages);
            }

            return clsUserAccess;
        }

        //Save
        public void saveUserAccess(clsUserAccess clsUserAccess)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblUserAccess tblUserAccess = new tblUserAccess();

                tblUserAccess.iUserAccessID = clsUserAccess.iUserAccessID;

                tblUserAccess.iUserID = clsUserAccess.iUserID;
                tblUserAccess.iPageID = clsUserAccess.iPageID;
                tblUserAccess.bIsRead = clsUserAccess.bIsRead;
                tblUserAccess.bIsWrite = clsUserAccess.bIsWrite;
                tblUserAccess.bIsDeleted = clsUserAccess.bIsDeleted;

                //Add
                if (tblUserAccess.iUserAccessID == 0)
                {
                    tblUserAccess.dtAdded = DateTime.Now;
                    tblUserAccess.iAddedBy = clsCMSUser.iCMSUserID;
                    tblUserAccess.dtEdited = DateTime.Now;
                    tblUserAccess.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblUserAccess.Add(tblUserAccess);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblUserAccess.dtAdded = clsUserAccess.dtAdded;
                    tblUserAccess.iAddedBy = clsUserAccess.iAddedBy;
                    tblUserAccess.dtEdited = DateTime.Now;
                    tblUserAccess.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblUserAccess>().AddOrUpdate(tblUserAccess);
                    db.SaveChanges();
                }
            }
        }

        //Remove
        public void removeUserAccessByID(int iUserAccessID)
        {
            tblUserAccess tblUserAccess = db.tblUserAccess.Find(iUserAccessID);
            if (tblUserAccess != null)
            {
                tblUserAccess.bIsDeleted = true;
                db.Entry(tblUserAccess).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check
        public bool checkIfUserAccessExists(int iUserAccessID)
        {
            bool bUserAccessExists = db.tblUserAccess.Any(UserAccess => UserAccess.iUserAccessID == iUserAccessID && UserAccess.bIsDeleted == false);
            return bUserAccessExists;
        }


        //Convert database table to class
        public clsUserAccess convertUserAccessTableToClass(tblUserAccess tblUserAccess)
        {
            clsUserAccess clsUserAccess = new clsUserAccess();

            clsUserAccess.iUserAccessID = tblUserAccess.iUserAccessID;
            clsUserAccess.dtAdded = tblUserAccess.dtAdded;
            clsUserAccess.iAddedBy = tblUserAccess.iAddedBy;
            clsUserAccess.dtEdited = tblUserAccess.dtEdited;
            clsUserAccess.iEditedBy = tblUserAccess.iEditedBy;

            clsUserAccess.iUserID = tblUserAccess.iUserID;
            clsUserAccess.iPageID = tblUserAccess.iPageID;
            clsUserAccess.bIsRead = tblUserAccess.bIsRead;
            clsUserAccess.bIsWrite = tblUserAccess.bIsWrite;
            clsUserAccess.bIsDeleted = tblUserAccess.bIsDeleted;

            return clsUserAccess;
        }
    }
}
