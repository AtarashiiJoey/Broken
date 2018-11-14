using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Colmart.Models;


namespace Colmart.View_Models
{
    public class clsProductSizesOrdered
    {
        public int iProductSizeID { get; set; }
        public DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int iEditedBy { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid size")]
        public string strSize { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public double dblPrice { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int iQuantityAvailable { get; set; }

        public int iQuantityOrderded { get; set; }
        public double dblSubTotal { get; set; }
        [Required(ErrorMessage = "Product is required")]
        public int iProductID { get; set; }
        public bool bIsDeleted { get; set; }

        public clsProducts clsProduct { get; set; }
    }
}