using Colmart.Models;
using System.Collections.Generic;

namespace Colmart.View_Models
{
    /// <summary>
    /// Model and properties for clsRecentlyViewed
    /// </summary>
    public class clsRecentlyViewed
    {
        public List<clsProducts> lstRecentlyViewed { get; set; }
        public bool bRecentlyViewed { get; set; }
    }
}