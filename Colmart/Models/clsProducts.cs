﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Colmart.Models
{
    /// <summary>
    /// Model and properties for clsProducts, with Foreign keys from DB
    /// </summary>
    public class clsProducts
    {

        public clsProducts()
        {
            clsLeads = new clsLeads();
            //clsUsers = new clsUsers();
        }

        public clsLeads clsLeads { get; set; }

        public int iProductID { get; set; }
        public DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int iEditedBy { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Title must be at least 2 characters long")]
        public string strTitle { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Remote("checkIfStyleCodeExists", "Products", HttpMethod = "POST", ErrorMessage = "Style Code already exists")]
        public string strStyleCode { get; set; }
        public string strProductColour { get; set; }
        public string strFullDescription { get; set; }
        public string strImageURL { get; set; }

        [Required(ErrorMessage = "Product Category is required")]
        public int iProductCategoryID { get; set; }

        [Required(ErrorMessage = "Product Sub Category is required")]
        public int iProductSubCategoryID { get; set; }
        public bool bIsDeleted { get; set; }

        public clsProductCategories clsProductCategory { get; set; }
        public clsProductSubCategories clsProductSubCategory { get; set; }
        public List<clsProductSizes> lstProductSizes { get; set; }
    }
}
