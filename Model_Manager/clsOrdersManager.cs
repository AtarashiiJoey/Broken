using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Colmart.Models;

namespace Colmart.Model_Manager
{
    public class clsOrdersManager
    {
        ColmartDBContext db = new ColmartDBContext();

        public List<clsOrders> getAllLeads()
        {
            var lstOrderlists = new List<clsOrders>();
            var lstGetOrdersList = db.tblOrders.Where(Order => Order.bIsDeleted == false).ToList();

            if (lstGetOrdersList.Any())
            {
                // Managers
                var clsOrderItemsManager = new clsOrderItemsManager();

                foreach (var orders in lstGetOrdersList)
                {
                    var clsorder = new clsOrders
                    {
                        iOrderID = orders.iOrderID,
                        dtAdded = orders.dtAdded,
                        iAddedBy = orders.iAddedBy,
                        dtEdited = orders.dtEdited,
                        iEditedBy = orders.iEditedBy,
                        iUserID = orders.iUserID,
                        iOrderStatusID = orders.iOrderStatusID,
                        bIsConfirmed = orders.bIsConfirmed,
                        bIsDeleted = orders.bIsDeleted
                    };

                    lstOrderlists.Add(clsorder);
                }
            }
            return lstOrderlists;
        }

        public int SaveLead(clsOrders clsOrders)
        {
            tblOrders tblOrders = new tblOrders
            {
                iOrderID = clsOrders.iOrderID,
                iUserID = clsOrders.iUserID,
                iOrderStatusID = clsOrders.iOrderStatusID,
                bIsConfirmed = clsOrders.bIsConfirmed,
                bIsDeleted = clsOrders.bIsDeleted
            };

            //Add
            if (tblOrders.iOrderID == 0)
            {
                tblOrders.dtAdded = DateTime.Now;
                tblOrders.iAddedBy = 1;
                tblOrders.dtEdited = DateTime.Now;
                tblOrders.iEditedBy = 1;

                db.tblOrders.Add(tblOrders);
                db.SaveChanges();
            }
            //Update
            else
            {
                tblOrders.dtAdded = clsOrders.dtAdded;
                tblOrders.iAddedBy = clsOrders.iAddedBy;
                tblOrders.dtEdited = DateTime.Now;
                tblOrders.iEditedBy = 1;

                db.Set<tblOrders>().AddOrUpdate(tblOrders);
                db.SaveChanges();
            }

            return tblOrders.iOrderID;
        }


        public clsOrders convertLeadTableToClass(tblOrders tblOrders)
        {
            var clsOrder = new clsOrders
            {
                iOrderID = tblOrders.iOrderID,
                dtAdded = tblOrders.dtAdded,
                iAddedBy = tblOrders.iAddedBy,
                dtEdited = tblOrders.dtEdited,
                iEditedBy = tblOrders.iEditedBy,
                iUserID = tblOrders.iUserID,
                iOrderStatusID = tblOrders.iOrderStatusID,
                bIsConfirmed = tblOrders.bIsConfirmed,
                bIsDeleted = tblOrders.bIsDeleted
            };
            return clsOrder;
        }
    }
}