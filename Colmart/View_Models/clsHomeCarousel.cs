using Colmart.Models;
using System.Collections.Generic;

namespace Colmart.View_Models
{
    /// <summary>
    /// Model and properties for clsHomeCarousel
    /// </summary>
    public class clsHomeCarousel
    {
        public List<clsProducts> lstTShirts { get; set; }
        public List<clsProducts> lstBottoms { get; set; }
        public List<clsProducts> lstJackets { get; set; }
        public List<clsProducts> lstFormal { get; set; }

    }
}