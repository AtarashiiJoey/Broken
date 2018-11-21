using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Colmart.Models
{
    public class clsAreas
    {
        public int iAreaID { get; set; }
        public DateTime? dtAdded { get; set; }
        public int? iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int? iEditedBy { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Please use 3 character IATA location abbreviation format")]
        public string strAreaAbbreviation { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Area name must be at least 2 characters long")]
        public string strAreaName { get; set; }

        public bool bIsDeleted { get; set; }

        public ICollection<clsUsers> clsUsers { get; set; }
    }
}