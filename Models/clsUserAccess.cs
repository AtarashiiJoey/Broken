using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colmart.Models
{
    public class clsUserAccess
    {
        public int iUserAccessID { get; set; }
        public DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public DateTime? dtEdited { get; set; }
        public int iEditedBy { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int iUserID { get; set; }
        [Required(ErrorMessage = "Page is required")]
        public int iPageID { get; set; }

        public bool bIsRead { get; set; }
        public bool bIsWrite { get; set; }
        public bool bIsDeleted { get; set; }

        public clsUsers clsUser { get; set; }
        public clsPages clsPage { get; set; }
    }
}
