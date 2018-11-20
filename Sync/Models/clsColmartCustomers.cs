using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colmart.Sync.Models
{
    public class clsColmartCustomers
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string AccountNo { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string VATNumber { get; set; }
        public string Terms { get; set; }
        public bool Active { get; set; }
        public string BranchName { get; set; }
    }
}