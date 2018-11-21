using System;
using Colmart.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Colmart.Model_Manager
{
    public class clsLeadsManager
    {
        ColmartDBContext db = new ColmartDBContext();

        public List<clsLeads> getAllLeads()
        {
            var lstLeadlists = new List<clsLeads>();
            var lstGetLeadsList = db.tblLeads.Where(Lead => Lead.bIsDeleted == false).ToList();

            if (lstGetLeadsList.Any())
            {
                // Managers
                var clsLeadWishlistsManager = new clsLeadWishlistsManager();

                foreach (var leads in lstGetLeadsList)
                {
                    clsLeads clsLead = new clsLeads();
                    clsLead.iLeadID = leads.iLeadID;
                    clsLead.dtAdded = leads.dtAdded;
                    clsLead.iAddedBy = leads.iAddedBy;
                    clsLead.dtEdited = leads.dtEdited;
                    clsLead.iEditedBy = leads.iEditedBy;
                    clsLead.strFirstName = leads.strFirstName;
                    clsLead.strEmail = leads.strEmail;
                    clsLead.strPhone = leads.strPhone;
                    clsLead.bIsDeleted = leads.bIsDeleted;
                    lstLeadlists.Add(clsLead);
                }
            }
            return lstLeadlists;
        }

        public int SaveLead(clsLeads clsLeads)
        {
           tblLeads tblLeads = new tblLeads();

            tblLeads.iLeadID = clsLeads.iLeadID;
            tblLeads.strFirstName = clsLeads.strFirstName;
            tblLeads.strEmail = clsLeads.strEmail;
            tblLeads.strPhone = clsLeads.strPhone;
            tblLeads.bIsDeleted = clsLeads.bIsDeleted;

            //Add
            if (tblLeads.iLeadID == 0)
            {
                tblLeads.dtAdded = DateTime.Now;
                tblLeads.iAddedBy = 1;
                tblLeads.dtEdited = DateTime.Now;
                tblLeads.iEditedBy = 1;

                db.tblLeads.Add(tblLeads);
                db.SaveChanges();
            }
            //Update
            else
            {
                tblLeads.dtAdded = clsLeads.dtAdded;
                tblLeads.iAddedBy = clsLeads.iAddedBy;
                tblLeads.dtEdited = DateTime.Now;
                tblLeads.iEditedBy = 1;

                db.Set<tblLeads>().AddOrUpdate(tblLeads);
                db.SaveChanges();
            }

            return tblLeads.iLeadID;
        }


        public clsLeads convertLeadTableToClass(tblLeads tblLeads)
        {
            var clsLead = new clsLeads();
            clsLead.iLeadID = tblLeads.iLeadID;
            clsLead.dtAdded = tblLeads.dtAdded;
            clsLead.iAddedBy = tblLeads.iAddedBy;
            clsLead.dtEdited = tblLeads.dtEdited;
            clsLead.iEditedBy = tblLeads.iEditedBy;
            clsLead.strFirstName = tblLeads.strFirstName;
            clsLead.strEmail = tblLeads.strEmail;
            clsLead.strPhone = tblLeads.strPhone;
            clsLead.bIsDeleted = tblLeads.bIsDeleted;
            return clsLead;
        }
    }
}