using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Colmart.Models
{
    public class clsOrderStatuses
    {
        public int iOrderStatusID { get; set; }
        public DateTime? dtAdded { get; set; }
        public int? iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int? iEditedBy { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Status message must be at least 2 characters long")]
        public string strOrderStatus { get; set; }

        public bool bIsDeleted { get; set; }

        public virtual ICollection<tblOrders> tblOrders { get; set; }
    }
}