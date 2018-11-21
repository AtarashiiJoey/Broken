using System;
using Colmart.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

        public void SaveLeadWishList(clsLeadWishlists clsLeadWishlists)
        {
            tblLeadWishlists tblLeadWishlists = new tblLeadWishlists();

            tblLeadWishlists.iLeadWishlistID = clsLeadWishlists.iLeadWishlistID;
            tblLeadWishlists.iLeadID = clsLeadWishlists.iLeadID;
            tblLeadWishlists.iProductID = clsLeadWishlists.iProductID;
            tblLeadWishlists.iProductSizeID = clsLeadWishlists.iProductSizeID;
            tblLeadWishlists.iProductQuantity = clsLeadWishlists.iProductQuantity;
            tblLeadWishlists.bIsDeleted = clsLeadWishlists.bIsDeleted;

            //Add
            if (tblLeadWishlists.iLeadWishlistID == 0)
            {
                tblLeadWishlists.dtAdded = DateTime.Now;
                tblLeadWishlists.iAddedBy = 1;
                tblLeadWishlists.dtEdited = DateTime.Now;
                tblLeadWishlists.iEditedBy = 1;

                db.tblLeadWishlists.Add(tblLeadWishlists);
                db.SaveChanges();
            }
            //Update
            else
            {
                tblLeadWishlists.dtAdded = clsLeadWishlists.dtAdded;
                tblLeadWishlists.iAddedBy = clsLeadWishlists.iAddedBy;
                tblLeadWishlists.dtEdited = DateTime.Now;
                tblLeadWishlists.iEditedBy = 1;

                db.Set<tblLeadWishlists>().AddOrUpdate(tblLeadWishlists);
                db.SaveChanges();
            }
        }

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