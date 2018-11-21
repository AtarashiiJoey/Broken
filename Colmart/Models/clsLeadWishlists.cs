using System;
using System.ComponentModel.DataAnnotations;

namespace Colmart.Models
{
    public class clsLeadWishlists
    {
        public int iLeadWishlistID { get; set; }
        public DateTime? dtAdded { get; set; }
        public int? iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int? iEditedBy { get; set; }

        public int iProductID { get; set; }
        public int iProductSizeID { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid amount")]
        public int iProductQuantity { get; set; }

        public int iLeadID { get; set; }
        public bool bIsDeleted { get; set; }

        public virtual clsLeads clsLead { get; set; }
        public virtual clsProducts clsProduct { get; set; }
        public virtual clsProductSizes clsProductSize { get; set; }
    }
}