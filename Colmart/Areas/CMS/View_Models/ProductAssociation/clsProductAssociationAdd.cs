using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colmart.Models;

namespace ColmartCMS.View_Models.ProductAssociation
{
    public class clsProductAssociationAdd
    {
        public clsProducts clsMainProducts { get; set; }
        public clsProducts clsAssociatedProducts { get; set; }
        public clsAssociations clsAssociations { get; set; }
        public IEnumerable<clsProducts> lstMainProducts { get; set; }
        public IEnumerable<clsProducts> lstAssociatedProducts { get; set; }
        public IEnumerable<clsAssociations> lstAssociations { get; set; }
        public clsProductAssociations clsProductAssociations { get; set; }
    }
}