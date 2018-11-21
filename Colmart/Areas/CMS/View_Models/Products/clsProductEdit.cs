using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colmart.Models;

namespace ColmartCMS.View_Models.Products
{
    public class clsProductEdit
    {
        public clsProductEdit()
        {
            clsProduct = new clsProducts();
        }
        public clsProducts clsProduct { get; set; }
        public List<clsProductCategories> lstProductCategories { get; set; }
        public List<clsProductSubCategories> lstProductSubCategories { get; set; }
    }
}
