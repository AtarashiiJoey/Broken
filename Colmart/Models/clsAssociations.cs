using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colmart.Models
{
    public class clsAssociations
    {
        public int iAssociationID { get; set; }
        public System.DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public System.DateTime dtEdited { get; set; }
        public int iEditedBy { get; set; }
        public string strTitle { get; set; }
        public bool bIsDeleted { get; set; }
    }
}