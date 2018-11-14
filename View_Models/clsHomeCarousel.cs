using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colmart.Models;

namespace Colmart.View_Models
{
    public class clsHomeCarousel
    {
        public List<clsProducts> lstTShirts { get; set; }
        public List<clsProducts> lstBottoms { get; set; }
        public List<clsProducts> lstJackets { get; set; }
        public List<clsProducts> lstFormal { get; set; }

    }
}