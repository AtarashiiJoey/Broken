using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using Colmart.Model_Manager;
using Colmart.View_Models;

namespace Colmart.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductDetails(int iProductID)
        {
            if (Session["clsUser"] != null)
                ViewBag.Layout = "_ColmartLayout.cshtml";
            else
                ViewBag.Layout = "_RolandoLayout.cshtml";
            var clsProductDetails = new clsProductDetails();
            var clsProductSizesManager = new clsProductSizesManager();
            var clsProductAssociationsManager = new clsProductAssociationsManager();
            var clsAssociationManager = new clsAssociationManager();
            var clsProductManager = new clsProductsManager();
            //clsProductDetails.ProductSizesList = clsProductSizesManager.getAllProductSizesListByID(iProductID);
            clsProductDetails.clsProducts = clsProductManager.getProductByID(iProductID);
            string strStyleCode = clsProductDetails.clsProducts.strStyleCode.Substring(0, 11);
            clsProductDetails.VariousProductSizesList = clsProductManager.getAllProductsByStyleCode(strStyleCode);
            clsProductDetails.lstProductAssociations = clsProductAssociationsManager.getAllProductAssociationsListByStyleCode(clsProductDetails.clsProducts.strStyleCode.Substring(0, 11));

            foreach(var association in clsProductDetails.lstProductAssociations)
            {
                association.clsAssociations = clsAssociationManager.getAssociationByID(association.iAssociationID);
            }

            int iSize;
            int iNewSize = 0;
            foreach(var product in clsProductDetails.VariousProductSizesList)
            {
                iSize = product.lstProductSizes.Count();
                if(iSize > iNewSize)
                {
                    iNewSize = iSize;
                }
            }
            if (iNewSize <= 8)
                ViewBag.HorizontalTable = "True";
            else
                ViewBag.HorizontalTable = "False";

            if (Request.Cookies["RecentlyViewed"] != null)
            {
                // Your cookie exists - grab your value and create your List
                List<int> recentlyViewedProducts = Request.Cookies["RecentlyViewed"].Value.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                if (recentlyViewedProducts.Contains(iProductID))
                {

                }
                else
                {
                    if(recentlyViewedProducts.Count >= 10)
                    {
                        recentlyViewedProducts.RemoveAt(0);
                    }
                    recentlyViewedProducts.Add(iProductID);
                }
                var recentlyViewedString = String.Join(",", recentlyViewedProducts);
                System.Web.HttpContext.Current.Response.Cookies.Remove("RecentlyViewed");
                HttpCookie RecentlyViewedCookie = new HttpCookie("RecentlyViewed", recentlyViewedString);
                // The cookie will exist for 7 days
                RecentlyViewedCookie.Expires = DateTime.Now.AddDays(30);

                // Write the Cookie to your Response
                Response.Cookies.Add(RecentlyViewedCookie);
            }
            else
            {
                // Build your list
                List<int> recentlyViewedProducts = new List<int>();
                if (recentlyViewedProducts.Contains(iProductID))
                {

                }
                else
                {
                    recentlyViewedProducts.Add(iProductID);
                }
                // Stringify your list
                var recentlyViewedString = String.Join(",", recentlyViewedProducts);

                // Create a cookie
                HttpCookie RecentlyViewedCookie = new HttpCookie("RecentlyViewed", recentlyViewedString);

                // The cookie will exist for 7 days
                RecentlyViewedCookie.Expires = DateTime.Now.AddDays(30);

                // Write the Cookie to your Response
                Response.Cookies.Add(RecentlyViewedCookie);
            }

            return View(clsProductDetails);
        }

        public JsonResult getVariationID(string strStyleCode)
        {
            clsProducts clsProducts = new clsProducts();
            clsProductsManager clsProductsManager = new clsProductsManager();
            clsProducts = clsProductsManager.getProductByStyleCode(strStyleCode);
            return Json(new { success = true, productID = clsProducts.iProductID});
        }
    }
}