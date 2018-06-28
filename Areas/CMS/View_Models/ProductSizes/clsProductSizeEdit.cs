using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colmart.Models;

namespace ColmartCMS.View_Models.ProductSizes
{
    public class clsProductSizeEdit
    {
        public clsProductSizeEdit()
        {
            clsProductSize = new clsProductSizes();
        }
        public clsProductSizes clsProductSize { get; set; }
    }
}
