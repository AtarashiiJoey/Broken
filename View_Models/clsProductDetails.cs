using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colmart.Models;

namespace Colmart.View_Models
{
    public class clsProductDetails
    {
        public clsProducts clsProducts { get; set; }
        public List<clsProductSizes> ProductSizesList { get; set; }
        public List<clsProducts> VariousProductSizesList { get; set; }
        public List<clsProductAssociations> lstProductAssociations { get; set; }
    }
}