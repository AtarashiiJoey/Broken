using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Colmart.Models;

namespace ColmartCMS.View_Models.ProductSizes
{
    public class clsProductSizeAdd
    {
        public clsProductSizeAdd()
        {
            clsProductSize = new clsProductSizes();
        }
        public clsProductSizes clsProductSize { get; set; }
        public List<clsProducts> lstProducts { get; set; }
    }
}
