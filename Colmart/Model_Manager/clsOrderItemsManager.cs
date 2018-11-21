using System.Collections.Generic;
using System.Linq;
using Colmart.Models;

namespace Colmart.Model_Manager
{
    public class clsOrderItemsManager
    {
        readonly ColmartDBContext db = new ColmartDBContext();

        public List<clsOrderItems> getAllProductSizesList()
        {
            var lstOrderItems = new List<clsOrderItems>();
            var lstGetOrderItemsList = db.tblOrderItems.Where(OrderItems => OrderItems.bIsDeleted == false).ToList();

            if (lstGetOrderItemsList.Any())
            {
                //Manager
                var clsOrderItemsManager = new clsOrderItemsManager();
                clsProductsManager clsProductsManager = new clsProductsManager();
                clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();

                foreach (var orderItems in lstGetOrderItemsList)
                {
                    var clsOrderItem = new clsOrderItems
                    {
                        iOrderItemID = orderItems.iOrderItemID,
                        dtAdded = orderItems.dtAdded,
                        iAddedBy = orderItems.iAddedBy,
                        dtEdited = orderItems.dtEdited,
                        iEditedBy = orderItems.iEditedBy,
                        iOrderID = orderItems.iOrderID,
                        iProductID = orderItems.iProductID,
                        iProductSizeID = orderItems.iProductSizeID,
                        iProductQuantity = orderItems.iProductQuantity,
                        bIsDeleted = orderItems.bIsDeleted
                    };

                    if (orderItems.tblProducts != null)
                        clsOrderItem.clsProducts = clsProductsManager.convertProductsTableToClass(orderItems.tblProducts);

                    if (orderItems.tblProductSizes != null)
                        clsOrderItem.clsProductSizes = clsProductSizesManager.convertProductSizesTableToClass(orderItems.tblProductSizes);
                }
            }

            return lstOrderItems;
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

        public clsOrderItems convertOrderItemsTableToClass(tblOrderItems tblOrderItems)
        {
            var clsOrderItems = new clsOrderItems
            {
                iOrderItemID = tblOrderItems.iOrderItemID,
                dtAdded = tblOrderItems.dtAdded,
                iAddedBy = tblOrderItems.iAddedBy,
                dtEdited = tblOrderItems.dtEdited,
                iEditedBy = tblOrderItems.iEditedBy,
                iOrderID = tblOrderItems.iOrderID,
                iProductID = tblOrderItems.iProductID,
                iProductSizeID = tblOrderItems.iProductSizeID,
                iProductQuantity = tblOrderItems.iProductQuantity,
                bIsDeleted = tblOrderItems.bIsDeleted
            };
            return clsOrderItems;
        }
    }
}