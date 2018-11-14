using Colmart.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Colmart.View_Models
{
    public class clsRecentlyViewed
    {
        public List<clsProducts> lstRecentlyViewed { get; set; }
        public bool bRecentlyViewed { get; set; }
    }
}