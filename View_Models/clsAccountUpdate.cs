using System;
using System.ComponentModel.DataAnnotations;

namespace Colmart.View_Models
{
    /// <summary>
    /// Model and properties for clsAccountUpdate
    /// </summary>
    public class clsAccountUpdate
    {
        public int iUserID { get; set; }
        public DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int iEditedBy { get; set; }

        [Required(ErrorMessage = "Role Type is required")]
        public int iRoleTypeID { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "First name must be at least 2 characters long")]
        public string strFirstName { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Last name must be at least 2 characters long")]
        public string strSurname { get; set; }
        public string strBiographicalInfo { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Incorrect contact number length")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid contact number")]
        public string strContactNumber { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string strEmailAddress { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string strCompanyName { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string strArea { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string strVatNumber { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string strBusinessPurpose { get; set; }
        [DataType(DataType.Password)]
        public string strPassword { get; set; }
        [DataType(DataType.Password)]
        public string strPasswordConfirm { get; set; }

        [DataType(DataType.Password)]
        public string strNewPassword { get; set; }
        [DataType(DataType.Password)]
        public string strConfirmNewPassword { get; set; }

        public string strImagePath { get; set; }
        public string strImageName { get; set; }
        public bool bIsConfirmed { get; set; }
        public bool bIsDeleted { get; set; }

    }
}