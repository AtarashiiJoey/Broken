using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colmart.Models;


namespace Colmart.Model_Manager
{
    public class clsProductAssociationsManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsProductAssociations> getAllProductAssociationsListByStyleCode(string strStyleCode)
        {
            List<clsProductAssociations> lstProductAssociations = new List<clsProductAssociations>();
            var lstGetProductAssociationsList = db.tblProductAssociationLinkTable.Where(productAssociation => productAssociation.iMainProductCode.Contains(strStyleCode) && productAssociation.bIsDeleted == false).ToList();

            if (lstGetProductAssociationsList.Count > 0)
            {
                foreach (var item in lstGetProductAssociationsList)
                {
                    clsProductAssociations clsProductAssociations = new clsProductAssociations();

                    clsProductAssociations.iProductLinkID = item.iProductLinkID;
                    clsProductAssociations.iMainProductCode = item.iMainProductCode;
                    clsProductAssociations.iAssociatedProductCode = item.iAssociatedProductCode;
                    clsProductAssociations.iAssociationID = item.iAssociationID;
                    clsProductAssociations.bIsDeleted = item.bIsDeleted;

                    lstProductAssociations.Add(clsProductAssociations);
                }
            }

            return lstProductAssociations;
        }
    }
}