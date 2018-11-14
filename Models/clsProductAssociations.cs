using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colmart.Models;

namespace Colmart.Models
{
    public class clsProductAssociations
    {
        public int iProductLinkID { get; set; }
        public string iMainProductCode { get; set; }
        public string iAssociatedProductCode { get; set; }
        public int iAssociationID { get; set; }
        public bool bIsDeleted { get; set; }

        public virtual clsAssociations clsAssociations { get; set; }
    }
}