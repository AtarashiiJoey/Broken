using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colmart.View_Models
{
    public class clsCartItems
    {
        public int iProductID { get; set; }
        public int iCartProductID { get; set; }
        public int iProductValue { get; set; }
        public int iStockAvailable { get; set; }
        public string strStyleCode { get; set; }
    }
}