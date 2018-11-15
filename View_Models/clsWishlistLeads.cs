using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colmart.Models;

namespace Colmart.View_Models
{
    public class clsWishlistLeads
    {
        public List<clsProducts> lstProducts { get; set; }
        public clsLeads clsLeads { get; set; }
    }
}