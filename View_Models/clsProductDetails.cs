using Colmart.Models;
using System.Collections.Generic;

namespace Colmart.View_Models
{
    /// <summary>
    /// Model and properties for clsProductDetails
    /// </summary>
    public class clsProductDetails
    {
        public clsProducts clsProducts { get; set; }
        public List<clsProductSizes> ProductSizesList { get; set; }
        public List<clsProducts> VariousProductSizesList { get; set; }
        public List<clsProductAssociations> lstProductAssociations { get; set; }
    }
}