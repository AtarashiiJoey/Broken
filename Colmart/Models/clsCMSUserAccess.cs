using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colmart.Models
{
    public class clsCMSUserAccess
    {
        public int iCMSUserAccessID { get; set; }
        public DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int iEditedBy { get; set; }

        [Required(ErrorMessage = "CMS User is required")]
        public int iCMSUserID { get; set; }

        [Required(ErrorMessage = "CMS Page is required")]
        public int iCMSPageID { get; set; }
        public bool bIsRead { get; set; }
        public bool bIsWrite { get; set; }
        public bool bIsDeleted { get; set; }

        public clsCMSUsers clsCMSUser { get; set; }
        public clsCMSPages clsCMSPage { get; set; }
    }
}
