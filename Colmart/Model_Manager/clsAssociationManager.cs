using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Colmart.Models;

namespace Colmart.Model_Manager
{
    public class clsAssociationManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsAssociations> getAllAssociationsList()
        {
            List<clsAssociations> lstAssociations = new List<clsAssociations>();
            var lstGetAssociationsList = db.tblAssociations.Where(association => association.bIsDeleted == false).ToList();

            if (lstGetAssociationsList.Count > 0)
            {
                foreach (var item in lstGetAssociationsList)
                {
                    clsAssociations clsAssociations = new clsAssociations();

                    clsAssociations.iAssociationID = item.iAssociationID;
                    clsAssociations.dtAdded = item.dtAdded;
                    clsAssociations.strTitle = item.strTitle;
                    clsAssociations.iAddedBy = item.iAddedBy;
                    clsAssociations.dtEdited = item.dtEdited;
                    clsAssociations.iEditedBy = item.iEditedBy;
                    clsAssociations.bIsDeleted = item.bIsDeleted;

                    lstAssociations.Add(clsAssociations);
                }
            }

            return lstAssociations;
        }

        public clsAssociations getAssociationByID(int iAssociationID)
        {
            clsAssociations clsAssociations = null;
            tblAssociations tblAssociations = db.tblAssociations.FirstOrDefault(association => association.iAssociationID == iAssociationID && association.bIsDeleted == false);

            if (tblAssociations != null)
            {
                clsAssociations = new clsAssociations();

                clsAssociations.iAssociationID = tblAssociations.iAssociationID;
                clsAssociations.dtAdded = tblAssociations.dtAdded;
                clsAssociations.iAddedBy = tblAssociations.iAddedBy;
                clsAssociations.dtEdited = tblAssociations.dtEdited;
                clsAssociations.iEditedBy = tblAssociations.iEditedBy;
                clsAssociations.strTitle = tblAssociations.strTitle;
                clsAssociations.bIsDeleted = tblAssociations.bIsDeleted;
            }

            return clsAssociations;
        } 

        //Save
        public void saveProductAssociation(clsProductAssociations clsProductAssociations)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblProductAssociationLinkTable tblProductAssociationLinkTable = new tblProductAssociationLinkTable();

                tblProductAssociationLinkTable.iProductLinkID = clsProductAssociations.iProductLinkID;
                tblProductAssociationLinkTable.iMainProductCode = clsProductAssociations.iMainProductCode;
                tblProductAssociationLinkTable.iAssociatedProductCode = clsProductAssociations.iAssociatedProductCode;
                tblProductAssociationLinkTable.iAssociationID = clsProductAssociations.iAssociationID;
                tblProductAssociationLinkTable.bIsDeleted = false;

                //Add
                if (tblProductAssociationLinkTable.iProductLinkID == 0)
                {
                    db.tblProductAssociationLinkTable.Add(tblProductAssociationLinkTable);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    db.Set<tblProductAssociationLinkTable>().AddOrUpdate(tblProductAssociationLinkTable);
                    db.SaveChanges();
                }
            }
        }

        //Check
        public bool checkIfProductAssociationExists(string strProductCode)
        {
            bool bProductAssociationExists = db.tblProductAssociationLinkTable.Any(association => association.iMainProductCode == strProductCode && association.bIsDeleted == false);
            return bProductAssociationExists;
        }

        //Check
        public bool checkIfSpecificProductAssociationExists(string strProductCode,string strAssociatedProductCode)
        {
            bool bProductAssociationExists = db.tblProductAssociationLinkTable.Any(association => association.iMainProductCode == strProductCode && association.iAssociatedProductCode == strAssociatedProductCode && association.bIsDeleted == false);
            return bProductAssociationExists;
        }
    }
}