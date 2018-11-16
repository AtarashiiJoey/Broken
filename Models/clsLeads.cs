using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Colmart.Models
{
    public class clsLeads
    {
        public int iLeadID { get; set; }
        public DateTime? dtAdded { get; set; }
        public int? iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int? iEditedBy { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "First name must be at least 2 characters long")]
        public string strFirstName { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        [Remote("checkIfEmailExists", "Login", HttpMethod = "POST", ErrorMessage = "Email already exists")]
        public string strEmail { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Incorrect contact number length")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid contact number")]
        public string strPhone { get; set; }
        
        public bool bIsDeleted { get; set; }

        // Foreign key thingies
        public virtual ICollection<tblLeadWishlists> tblLeadWishlists { get; set; }
    }
}