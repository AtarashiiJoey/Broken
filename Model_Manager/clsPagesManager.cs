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
    public class clsPagesManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsPages> getAllPagesList()
        {
            List<clsPages> lstPages = new List<clsPages>();
            var lstGetPagesList = db.tblPages.Where(Page => Page.bIsDeleted == false).ToList();

            if (lstGetPagesList.Count > 0)
            {
                clsUserAccessManager clsUserAccessManager = new clsUserAccessManager(); //User Access Manager

                foreach (var item in lstGetPagesList)
                {
                    clsPages clsPage = new clsPages();

                    clsPage.iPageID = item.iPageID;
                    clsPage.dtAdded = item.dtAdded;
                    clsPage.iAddedBy = item.iAddedBy;
                    clsPage.dtEdited = item.dtEdited;
                    clsPage.iEditedBy = item.iEditedBy;

                    clsPage.strTitle = item.strTitle;
                    clsPage.bIsDeleted = item.bIsDeleted;

                    clsPage.lstUserAccess = new List<clsUserAccess>();

                    if (item.tblUserAccess.Count > 0)
                    {
                        foreach (var UserAccessItem in item.tblUserAccess)
                        {
                            clsUserAccess clsUserAccess = clsUserAccessManager.convertUserAccessTableToClass(UserAccessItem);
                            clsPage.lstUserAccess.Add(clsUserAccess);
                        }
                    }

                    lstPages.Add(clsPage);
                }
            }

            return lstPages;
        }

        //Get All
        public List<clsPages> getAllPagesOnlyList()
        {
            List<clsPages> lstPages = new List<clsPages>();
            var lstGetPagesList = db.tblPages.Where(Page => Page.bIsDeleted == false).ToList();

            if (lstGetPagesList.Count > 0)
            {
                clsUserAccessManager clsUserAccessManager = new clsUserAccessManager(); //User Access Manager

                foreach (var item in lstGetPagesList)
                {
                    clsPages clsPage = new clsPages();

                    clsPage.iPageID = item.iPageID;
                    clsPage.dtAdded = item.dtAdded;
                    clsPage.iAddedBy = item.iAddedBy;
                    clsPage.dtEdited = item.dtEdited;
                    clsPage.iEditedBy = item.iEditedBy;

                    clsPage.strTitle = item.strTitle;
                    clsPage.bIsDeleted = item.bIsDeleted;

                    clsPage.lstUserAccess = new List<clsUserAccess>();

                    lstPages.Add(clsPage);
                }
            }

            return lstPages;
        }

        //Get
        public clsPages getPageByID(int iPageID)
        {
            clsPages clsPage = null;
            tblPages tblPage = db.tblPages.FirstOrDefault(page => page.iPageID == iPageID && page.bIsDeleted == false);

            if (tblPage != null)
            {
                clsUserAccessManager clsUserAccessManager = new clsUserAccessManager(); //User Access Manager

                clsPage = new clsPages();
                clsPage.iPageID = tblPage.iPageID;
                clsPage.dtAdded = tblPage.dtAdded;
                clsPage.iAddedBy = tblPage.iAddedBy;
                clsPage.dtEdited = tblPage.dtEdited;
                clsPage.iEditedBy = tblPage.iEditedBy;

                clsPage.strTitle = tblPage.strTitle;
                clsPage.bIsDeleted = tblPage.bIsDeleted;

                clsPage.lstUserAccess = new List<clsUserAccess>();

                if (tblPage.tblUserAccess.Count > 0)
                {
                    foreach (var UserAccessItem in tblPage.tblUserAccess)
                    {
                        clsUserAccess clsUserAccess = clsUserAccessManager.convertUserAccessTableToClass(UserAccessItem);
                        clsPage.lstUserAccess.Add(clsUserAccess);
                    }
                }
            }

            return clsPage;
        }

        //Save
        public void savePage(clsPages clsPage)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblPages tblPage = new tblPages();

                tblPage.iPageID = clsPage.iPageID;

                tblPage.strTitle = clsPage.strTitle;
                tblPage.bIsDeleted = clsPage.bIsDeleted;

                //Add
                if (tblPage.iPageID == 0)
                {
                    tblPage.dtAdded = DateTime.Now;
                    tblPage.iAddedBy = clsCMSUser.iCMSUserID;
                    tblPage.dtEdited = DateTime.Now;
                    tblPage.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblPages.Add(tblPage);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblPage.dtAdded = clsPage.dtAdded;
                    tblPage.iAddedBy = clsPage.iAddedBy;
                    tblPage.dtEdited = DateTime.Now;
                    tblPage.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblPages>().AddOrUpdate(tblPage);
                    db.SaveChanges();
                }
            }
        }

        //Remove
        public void removePageByID(int iPageID)
        {
            tblPages tblPage = db.tblPages.Find(iPageID);
            if (tblPage != null)
            {
                tblPage.bIsDeleted = true;
                db.Entry(tblPage).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check
        public bool checkIfPageExists(int iPageID)
        {
            bool bPageExists = db.tblPages.Any(page => page.iPageID == iPageID && page.bIsDeleted == false);
            return bPageExists;
        }

        //Convert database table to class
        public clsPages convertPagesTableToClass(tblPages tblPage)
        {
            clsPages clsPage = new clsPages();

            clsPage.iPageID = tblPage.iPageID;
            clsPage.dtAdded = tblPage.dtAdded;
            clsPage.iAddedBy = tblPage.iAddedBy;
            clsPage.dtEdited = tblPage.dtEdited;
            clsPage.iEditedBy = tblPage.iEditedBy;

            clsPage.strTitle = tblPage.strTitle;
            clsPage.bIsDeleted = tblPage.bIsDeleted;

            return clsPage;
        }
    }
}
