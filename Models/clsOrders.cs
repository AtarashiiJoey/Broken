using System;
using System.Collections.Generic;

namespace Colmart.Models
{
    public class clsOrders
    {
        public int iOrderID { get; set; }
        public System.DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int? iEditedBy { get; set; }

        public int iUserID { get; set; }
        public int iOrderStatusID { get; set; }

        public bool bIsConfirmed { get; set; }
        public bool bIsDeleted { get; set; }

        public virtual ICollection<clsOrderItems> clsOrderItems { get; set; }
        public virtual clsOrderStatuses clsOrderStatuses { get; set; }
        public virtual clsUsers clsUsers { get; set; }
    }
}