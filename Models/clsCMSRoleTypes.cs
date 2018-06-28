﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Colmart.Models
{
    public class clsCMSRoleTypes
    {
        public int iCMSRoleTypeID { get; set; }
        public DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int iEditedBy { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Title must be at least 2 characters long")]
        [Remote("checkIfCMSRoleTypeExists", "CMSRoleTypes", HttpMethod = "POST", ErrorMessage = "Role type already exists")]
        public string strTitle { get; set; }
        public bool bIsDeleted { get; set; }

        public List<clsCMSUsers> lstCMSUsers { get; set; }
    }
}
