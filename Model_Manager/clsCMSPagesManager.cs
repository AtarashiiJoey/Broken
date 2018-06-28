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
    public class clsCMSPagesManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsCMSPages> getAllCMSPagesList()
        {
            List<clsCMSPages> lstCMSPages = new List<clsCMSPages>();
            var lstGetCMSPagesList = db.tblCMSPages.Where(CMSPage => CMSPage.bIsDeleted == false).ToList();

            if (lstGetCMSPagesList.Count > 0)
            {
                //CMS User Access Manager
                clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager();

                foreach (var item in lstGetCMSPagesList)
                {
                    clsCMSPages clsCMSPage = new clsCMSPages();

                    clsCMSPage.iCMSPageID = item.iCMSPageID;
                    clsCMSPage.dtAdded = item.dtAdded;
                    clsCMSPage.iAddedBy = item.iAddedBy;
                    clsCMSPage.dtEdited = item.dtEdited;
                    clsCMSPage.iEditedBy = item.iEditedBy;

                    clsCMSPage.strTitle = item.strTitle;
                    clsCMSPage.bIsDeleted = item.bIsDeleted;

                    clsCMSPage.lstCMSUserAccess = new List<clsCMSUserAccess>();

                    if (item.tblCMSUserAccess.Count > 0)
                    {
                        foreach (var CMSUserAccessItem in item.tblCMSUserAccess)
                        {
                            clsCMSUserAccess clsCMSUserAccess = clsCMSUserAccessManager.convertCMSUserAccessTableToClass(CMSUserAccessItem);
                            clsCMSPage.lstCMSUserAccess.Add(clsCMSUserAccess);
                        }
                    }

                    lstCMSPages.Add(clsCMSPage);
                }
            }

            return lstCMSPages;
        }

        //Get All
        public List<clsCMSPages> getAllCMSPagesOnlyList()
        {
            List<clsCMSPages> lstCMSPages = new List<clsCMSPages>();
            var lstGetCMSPagesList = db.tblCMSPages.Where(CMSPage => CMSPage.bIsDeleted == false).ToList();

            if (lstGetCMSPagesList.Count > 0)
            {
                //CMS User Access Manager
                clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager();

                foreach (var item in lstGetCMSPagesList)
                {
                    clsCMSPages clsCMSPage = new clsCMSPages();

                    clsCMSPage.iCMSPageID = item.iCMSPageID;
                    clsCMSPage.dtAdded = item.dtAdded;
                    clsCMSPage.iAddedBy = item.iAddedBy;
                    clsCMSPage.dtEdited = item.dtEdited;
                    clsCMSPage.iEditedBy = item.iEditedBy;

                    clsCMSPage.strTitle = item.strTitle;
                    clsCMSPage.bIsDeleted = item.bIsDeleted;

                    clsCMSPage.lstCMSUserAccess = new List<clsCMSUserAccess>();

                    lstCMSPages.Add(clsCMSPage);
                }
            }

            return lstCMSPages;
        }

        //Get
        public clsCMSPages getCMSPageByID(int iCMSPageID)
        {
            clsCMSPages clsCMSPage = null;
            tblCMSPages tblCMSPages = db.tblCMSPages.FirstOrDefault(CMSPage => CMSPage.iCMSPageID == iCMSPageID && CMSPage.bIsDeleted == false);

            if (tblCMSPages != null)
            {
                //CMS User Access Manager
                clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager();

                clsCMSPage = new clsCMSPages();

                clsCMSPage.iCMSPageID = tblCMSPages.iCMSPageID;
                clsCMSPage.dtAdded = tblCMSPages.dtAdded;
                clsCMSPage.iAddedBy = tblCMSPages.iAddedBy;
                clsCMSPage.dtEdited = tblCMSPages.dtEdited;
                clsCMSPage.iEditedBy = tblCMSPages.iEditedBy;

                clsCMSPage.strTitle = tblCMSPages.strTitle;
                clsCMSPage.bIsDeleted = tblCMSPages.bIsDeleted;

                clsCMSPage.lstCMSUserAccess = new List<clsCMSUserAccess>();

                if (tblCMSPages.tblCMSUserAccess.Count > 0)
                {
                    foreach (var CMSUserAccessItem in tblCMSPages.tblCMSUserAccess)
                    {
                        clsCMSUserAccess clsCMSUserAccess = clsCMSUserAccessManager.convertCMSUserAccessTableToClass(CMSUserAccessItem);
                        clsCMSPage.lstCMSUserAccess.Add(clsCMSUserAccess);
                    }
                }
            }

            return clsCMSPage;
        }

        //Save
        public void saveCMSPage(clsCMSPages clsCMSPage)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblCMSPages tblCMSPages = new tblCMSPages();

                tblCMSPages.iCMSPageID = clsCMSPage.iCMSPageID;

                tblCMSPages.strTitle = clsCMSPage.strTitle;
                tblCMSPages.bIsDeleted = clsCMSPage.bIsDeleted;

                //Add
                if (tblCMSPages.iCMSPageID == 0)
                {
                    tblCMSPages.dtAdded = DateTime.Now;
                    tblCMSPages.iAddedBy = clsCMSUser.iCMSUserID;
                    tblCMSPages.dtEdited = DateTime.Now;
                    tblCMSPages.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblCMSPages.Add(tblCMSPages);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblCMSPages.dtAdded = clsCMSPage.dtAdded;
                    tblCMSPages.iAddedBy = clsCMSPage.iAddedBy;
                    tblCMSPages.dtEdited = DateTime.Now;
                    tblCMSPages.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblCMSPages>().AddOrUpdate(tblCMSPages);
                    db.SaveChanges();
                }
            }
        }

        //Remove
        public void removeCMSPageByID(int iCMSPageID)
        {
            tblCMSPages tblCMSPage = db.tblCMSPages.Find(iCMSPageID);
            if (tblCMSPage != null)
            {
                tblCMSPage.bIsDeleted = true;
                db.Entry(tblCMSPage).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check
        public bool checkIfCMSPageExists(int iCMSPageID)
        {
            bool bCMSPageExists = db.tblCMSPages.Any(CMSPage => CMSPage.iCMSPageID == iCMSPageID && CMSPage.bIsDeleted == false);
            return bCMSPageExists;
        }

        //Convert database table to class
        public clsCMSPages convertCMSPagesTableToClass(tblCMSPages tblCMSPage)
        {
            clsCMSPages clsCMSPage = new clsCMSPages();

            clsCMSPage.iCMSPageID = tblCMSPage.iCMSPageID;
            clsCMSPage.dtAdded = tblCMSPage.dtAdded;
            clsCMSPage.iAddedBy = tblCMSPage.iAddedBy;
            clsCMSPage.dtEdited = tblCMSPage.dtEdited;
            clsCMSPage.iEditedBy = tblCMSPage.iEditedBy;

            clsCMSPage.strTitle = tblCMSPage.strTitle;
            clsCMSPage.bIsDeleted = tblCMSPage.bIsDeleted;

            return clsCMSPage;
        }
    }
}
