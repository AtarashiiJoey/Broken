using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colmart.Sync.Models
{
    public class clsColmartProducts
    {
        public int fProductStockID { get; set; }
        public string Branch { get; set; }
        public string StyleCode { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string ImageURL { get; set; }
        public string Size { get; set; }
        public int SizeOrdinal { get; set; }
        public decimal? Price { get; set; }
        public decimal? Qty_OnHand { get; set; }
    }
}