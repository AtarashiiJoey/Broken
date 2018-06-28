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
    public class clsCMSUserAccessManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsCMSUserAccess> getAllCMSUserAccessList()
        {
            List<clsCMSUserAccess> lstCMSUserAccess = new List<clsCMSUserAccess>();
            var lstGetCMSUserAccessList = db.tblCMSUserAccess.Where(CMSUserAccess => CMSUserAccess.bIsDeleted == false).ToList();

            if (lstGetCMSUserAccessList.Count > 0)
            {
                clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager(); //CMS User Manager
                clsCMSPagesManager clsCMSPagesManager = new clsCMSPagesManager(); //CMS Pages Manger

                foreach (var item in lstGetCMSUserAccessList)
                {
                    clsCMSUserAccess clsCMSUserAccess = new clsCMSUserAccess();

                    clsCMSUserAccess.iCMSUserAccessID = item.iCMSUserAccessID;
                    clsCMSUserAccess.dtAdded = item.dtAdded;
                    clsCMSUserAccess.iAddedBy = item.iAddedBy;
                    clsCMSUserAccess.dtEdited = item.dtEdited;
                    clsCMSUserAccess.iEditedBy = item.iEditedBy;

                    clsCMSUserAccess.iCMSUserID = item.iCMSUserID;
                    clsCMSUserAccess.iCMSPageID = item.iCMSPageID;
                    clsCMSUserAccess.bIsRead = item.bIsRead;
                    clsCMSUserAccess.bIsWrite = item.bIsWrite;
                    clsCMSUserAccess.bIsDeleted = item.bIsDeleted;

                    if (item.tblCMSUsers != null)
                        clsCMSUserAccess.clsCMSUser = clsCMSUsersManager.convertCMSUsersTableToClass(item.tblCMSUsers);
                    if (item.tblCMSPages != null)
                        clsCMSUserAccess.clsCMSPage = clsCMSPagesManager.convertCMSPagesTableToClass(item.tblCMSPages);

                    lstCMSUserAccess.Add(clsCMSUserAccess);
                }
            }

            return lstCMSUserAccess;
        }

        //Get All
        public List<clsCMSUserAccess> getAllCMSUserAccessOnlyList()
        {
            List<clsCMSUserAccess> lstCMSUserAccess = new List<clsCMSUserAccess>();
            var lstGetCMSUserAccessList = db.tblCMSUserAccess.Where(CMSUserAccess => CMSUserAccess.bIsDeleted == false).ToList();

            if (lstGetCMSUserAccessList.Count > 0)
            {
                clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager(); //CMS User Manager
                clsCMSPagesManager clsCMSPagesManager = new clsCMSPagesManager(); //CMS Pages Manger

                foreach (var item in lstGetCMSUserAccessList)
                {
                    clsCMSUserAccess clsCMSUserAccess = new clsCMSUserAccess();

                    clsCMSUserAccess.iCMSUserAccessID = item.iCMSUserAccessID;
                    clsCMSUserAccess.dtAdded = item.dtAdded;
                    clsCMSUserAccess.iAddedBy = item.iAddedBy;
                    clsCMSUserAccess.dtEdited = item.dtEdited;
                    clsCMSUserAccess.iEditedBy = item.iEditedBy;

                    clsCMSUserAccess.iCMSUserID = item.iCMSUserID;
                    clsCMSUserAccess.iCMSPageID = item.iCMSPageID;
                    clsCMSUserAccess.bIsRead = item.bIsRead;
                    clsCMSUserAccess.bIsWrite = item.bIsWrite;
                    clsCMSUserAccess.bIsDeleted = item.bIsDeleted;

                    lstCMSUserAccess.Add(clsCMSUserAccess);
                }
            }

            return lstCMSUserAccess;
        }

        //Get
        public clsCMSUserAccess getCMSUserAccessByID(int iCMSUserAccessID)
        {
            clsCMSUserAccess clsCMSUserAccess = null;
            tblCMSUserAccess tblCMSUserAccess = db.tblCMSUserAccess.FirstOrDefault(CMSUserAccess => CMSUserAccess.iCMSUserAccessID == iCMSUserAccessID && CMSUserAccess.bIsDeleted == false);

            if (tblCMSUserAccess != null)
            {
                clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager(); //CMS User Manager
                clsCMSPagesManager clsCMSPagesManager = new clsCMSPagesManager(); //CMS Pages Manger

                clsCMSUserAccess = new clsCMSUserAccess();

                clsCMSUserAccess.iCMSUserAccessID = tblCMSUserAccess.iCMSUserAccessID;
                clsCMSUserAccess.dtAdded = tblCMSUserAccess.dtAdded;
                clsCMSUserAccess.iAddedBy = tblCMSUserAccess.iAddedBy;
                clsCMSUserAccess.dtEdited = tblCMSUserAccess.dtEdited;
                clsCMSUserAccess.iEditedBy = tblCMSUserAccess.iEditedBy;

                clsCMSUserAccess.iCMSUserID = tblCMSUserAccess.iCMSUserID;
                clsCMSUserAccess.iCMSPageID = tblCMSUserAccess.iCMSPageID;
                clsCMSUserAccess.bIsRead = tblCMSUserAccess.bIsRead;
                clsCMSUserAccess.bIsWrite = tblCMSUserAccess.bIsWrite;
                clsCMSUserAccess.bIsDeleted = tblCMSUserAccess.bIsDeleted;

                if (tblCMSUserAccess.tblCMSUsers != null)
                    clsCMSUserAccess.clsCMSUser = clsCMSUsersManager.convertCMSUsersTableToClass(tblCMSUserAccess.tblCMSUsers);
                if (tblCMSUserAccess.tblCMSPages != null)
                    clsCMSUserAccess.clsCMSPage = clsCMSPagesManager.convertCMSPagesTableToClass(tblCMSUserAccess.tblCMSPages);
            }

            return clsCMSUserAccess;
        }

        //Save
        public void saveCMSUserAccess(clsCMSUserAccess clsCMSUserAccess)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblCMSUserAccess tblCMSUserAccess = new tblCMSUserAccess();

                tblCMSUserAccess.iCMSUserAccessID = clsCMSUserAccess.iCMSUserAccessID;

                tblCMSUserAccess.iCMSUserID = clsCMSUserAccess.iCMSUserID;
                tblCMSUserAccess.iCMSPageID = clsCMSUserAccess.iCMSPageID;
                tblCMSUserAccess.bIsRead = clsCMSUserAccess.bIsRead;
                tblCMSUserAccess.bIsWrite = clsCMSUserAccess.bIsWrite;
                tblCMSUserAccess.bIsDeleted = clsCMSUserAccess.bIsDeleted;

                //Add
                if (tblCMSUserAccess.iCMSUserAccessID == 0)
                {
                    tblCMSUserAccess.dtAdded = DateTime.Now;
                    tblCMSUserAccess.iAddedBy = clsCMSUser.iCMSUserID;
                    tblCMSUserAccess.dtEdited = DateTime.Now;
                    tblCMSUserAccess.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblCMSUserAccess.Add(tblCMSUserAccess);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblCMSUserAccess.dtAdded = clsCMSUserAccess.dtAdded;
                    tblCMSUserAccess.iAddedBy = clsCMSUserAccess.iAddedBy;
                    tblCMSUserAccess.dtEdited = DateTime.Now;
                    tblCMSUserAccess.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblCMSUserAccess>().AddOrUpdate(tblCMSUserAccess);
                    db.SaveChanges();
                }
            }
        }

        //Remove
        public void removeCMSUserAccessByID(int iCMSUserAccessID)
        {
            tblCMSUserAccess tblCMSUserAccess = db.tblCMSUserAccess.Find(iCMSUserAccessID);
            if (tblCMSUserAccess != null)
            {
                tblCMSUserAccess.bIsDeleted = true;
                db.Entry(tblCMSUserAccess).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check
        public bool checkIfCMSUserAccessExists(int iCMSUserAccessID)
        {
            bool bCMSUserAccessExists = db.tblCMSUserAccess.Any(CMSUserAccess => CMSUserAccess.iCMSUserAccessID == iCMSUserAccessID && CMSUserAccess.bIsDeleted == false);
            return bCMSUserAccessExists;
        }

        //Convert database table to class
        public clsCMSUserAccess convertCMSUserAccessTableToClass(tblCMSUserAccess tblCMSUserAccess)
        {
            clsCMSUserAccess clsCMSUserAccess = new clsCMSUserAccess();

            clsCMSUserAccess.iCMSUserAccessID = tblCMSUserAccess.iCMSUserAccessID;
            clsCMSUserAccess.dtAdded = tblCMSUserAccess.dtAdded;
            clsCMSUserAccess.iAddedBy = tblCMSUserAccess.iAddedBy;
            clsCMSUserAccess.dtEdited = tblCMSUserAccess.dtEdited;
            clsCMSUserAccess.iEditedBy = tblCMSUserAccess.iEditedBy;

            clsCMSUserAccess.iCMSUserID = tblCMSUserAccess.iCMSUserID;
            clsCMSUserAccess.iCMSPageID = tblCMSUserAccess.iCMSPageID;
            clsCMSUserAccess.bIsRead = tblCMSUserAccess.bIsRead;
            clsCMSUserAccess.bIsWrite = tblCMSUserAccess.bIsWrite;
            clsCMSUserAccess.bIsDeleted = tblCMSUserAccess.bIsDeleted;

            return clsCMSUserAccess;
        }
    }
}
