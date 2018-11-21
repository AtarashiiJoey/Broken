using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        public string strPrimaryContact { get; set; }
        public string strSecondaryContact { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid contact number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Incorrect contact number length")]
        public string strPrimaryContactNumber { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid contact number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Incorrect contact number length")]
        public string strSecondaryContactNumber { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        [Remote("checkIfUserExists", "Users", HttpMethod = "POST", ErrorMessage = "Email already exists")]
        public string strEmailAddress { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string strCompanyName { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public int? iAreaID { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string strVatNumber { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string strTerms { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string strBusinessPurpose { get; set; }
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters long")]
        public string strNewPassword { get; set; }
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters long")]
        [System.ComponentModel.DataAnnotations.Compare("strNewPassword", ErrorMessage = "Password does not match")]
        public string strConfirmNewPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters long")]
        public string strPassword { get; set; }
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters long")]
        [System.ComponentModel.DataAnnotations.Compare("strPassword", ErrorMessage = "Password does not match")]
        public string strPasswordConfirm { get; set; }

        public string strImagePath { get; set; }
        public string strImageName { get; set; }
        public bool bIsConfirmed { get; set; }
        public bool bIsDeleted { get; set; }

    }
}