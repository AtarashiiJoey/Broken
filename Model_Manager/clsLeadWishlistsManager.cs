using Colmart.Models;
using System.Collections.Generic;
using System.Linq;

namespace Colmart.Model_Manager
{
    public class clsLeadWishlistsManager
    {
        readonly ColmartDBContext db = new ColmartDBContext();

        public List<clsLeadWishlists> getAllProductSizesList()
        {
            var lstLeadWishlists = new List<clsLeadWishlists>();
            var lstGetLeadWishlistsList = db.tblLeadWishlists.Where(Wishlists => Wishlists.bIsDeleted == false).ToList();

            if (lstGetLeadWishlistsList.Any())
            {
                //Manager
                var clsLeadsManager = new clsLeadsManager();
                clsProductsManager clsProductsManager = new clsProductsManager();
                clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();

                foreach (var wish in lstGetLeadWishlistsList)
                {
                    var clsLeadWishlist = new clsLeadWishlists
                    {
                        iLeadWishlistID = wish.iProductSizeID,
                        dtAdded = wish.dtAdded,
                        iAddedBy = wish.iAddedBy,
                        dtEdited = wish.dtEdited,
                        iEditedBy = wish.iEditedBy,
                        iProductID = wish.iProductID,
                        iProductSizeID = wish.iProductSizeID,
                        iProductQuantity = wish.iProductQuantity,
                        iLeadID = wish.iLeadID,
                        bIsDeleted = wish.bIsDeleted
                    };

                    if (wish.tblProducts != null)
                        clsLeadWishlist.clsProduct = clsProductsManager.convertProductsTableToClass(wish.tblProducts);

                    if (wish.tblProductSizes != null)
                        clsLeadWishlist.clsProductSize = clsProductSizesManager.convertProductSizesTableToClass(wish.tblProductSizes);
                }
            }

            return lstLeadWishlists;
        }

        //public void SaveLeadWishList(int iWishlist)
        //{
        //    tblLeads tblLeads = new tblLeads();

        //    tblLeads.iLeadID = clsLeads.iLeadID;
        //    tblLeads.strFirstName = clsLeads.strFirstName;
        //    tblLeads.strEmail = clsLeads.strEmail;
        //    tblLeads.strPhone = clsLeads.strPhone;
        //    tblLeads.bIsDeleted = clsLeads.bIsDeleted;

        //    //Add
        //    if (tblLeads.iLeadID == 0)
        //    {
        //        tblLeads.dtAdded = DateTime.Now;
        //        tblLeads.iAddedBy = 1;
        //        tblLeads.dtEdited = DateTime.Now;
        //        tblLeads.iEditedBy = 1;

        //        db.tblLeads.Add(tblLeads);
        //        db.SaveChanges();
        //    }
        //    //Update
        //    else
        //    {
        //        tblLeads.dtAdded = clsLeads.dtAdded;
        //        tblLeads.iAddedBy = clsLeads.iAddedBy;
        //        tblLeads.dtEdited = DateTime.Now;
        //        tblLeads.iEditedBy = 1;

        //        db.Set<tblLeads>().AddOrUpdate(tblLeads);
        //        db.SaveChanges();
        //    }

        //    return tblLeads.iLeadID;
        //}

        public clsLeadWishlists convertWishlistsTableToClass(tblLeadWishlists tblWishlists)
        {
            var clsWishlists = new clsLeadWishlists
            {
                iLeadWishlistID = tblWishlists.iLeadWishlistID,
                dtAdded = tblWishlists.dtAdded,
                iAddedBy = tblWishlists.iAddedBy,
                dtEdited = tblWishlists.dtEdited,
                iEditedBy = tblWishlists.iEditedBy,
                iProductID = tblWishlists.iProductID,
                iProductSizeID = tblWishlists.iProductSizeID,
                iProductQuantity = tblWishlists.iProductQuantity,
                iLeadID = tblWishlists.iLeadID,
                bIsDeleted = tblWishlists.bIsDeleted
            };
            return clsWishlists;
        }
    }

}