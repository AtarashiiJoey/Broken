using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using Colmart.Model_Manager;

namespace Colmart.Controllers
{
    public class WishlistController : Controller
    {
        // GET: Wishlist
        /// <summary>
        ///  Get the Wishlist using the clsProductsManager class, and populating a list of wishListProducts
        /// </summary>
        /// <returns>wishListProducts as a view</returns>
        public ActionResult Wishlist()
        {
            clsProductsManager clsProductsManager = new clsProductsManager();
            List<clsProducts> wishListProducts = new List<clsProducts>();
            if (Request.Cookies["WishList"] != null)
            {
                // Your cookie exists - grab your value and create your List
                List<int> productWishList = Request.Cookies["WishList"].Value.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                foreach(var product in productWishList)
                {
                    clsProducts clsWishListProduct;
                    clsWishListProduct = clsProductsManager.getProductByID(product);
                    wishListProducts.Add(clsWishListProduct);
                }
            }
            if (Session["clsUser"] != null)
                ViewBag.Layout = "_ColmartLayout.cshtml";
            else
                ViewBag.Layout = "_RolandoLayout.cshtml";
            return View(wishListProducts);
        }
        
        /// <summary>
        /// Cookie management for the Wishlist, Adds a new cookie if current cookie is stale (older than 7 days)
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns>JsonResult to HttpResponse</returns>
        public JsonResult addToWishlist(int iProductID)
        {
            if (Request.Cookies["WishList"] != null)
            {
                // Your cookie exists - grab your value and create your List
                List<int> productWishList = Request.Cookies["WishList"].Value.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                if(productWishList.Contains(iProductID))
                {

                }
                else { 
                productWishList.Add(iProductID);
                }
                var WishListString = String.Join(",", productWishList);
                System.Web.HttpContext.Current.Response.Cookies.Remove("WishList");
                HttpCookie WishListCookie = new HttpCookie("WishList", WishListString);
                // The cookie will exist for 7 days
                WishListCookie.Expires = DateTime.Now.AddDays(30);

                // Write the Cookie to your Response
                Response.Cookies.Add(WishListCookie);
            }
            else
            {
                // Build your list
                List<int> productWishList = new List<int>();
                if (productWishList.Contains(iProductID))
                {

                }
                else
                {
                    productWishList.Add(iProductID);
                }
                // Stringify your list
                var WishListString = String.Join(",", productWishList);

                // Create a cookie
                HttpCookie WishListCookie = new HttpCookie("WishList", WishListString);

                // The cookie will exist for 7 days
                WishListCookie.Expires = DateTime.Now.AddDays(30);

                // Write the Cookie to your Response
                Response.Cookies.Add(WishListCookie);
            }
            return Json(new { success = true, message = "The product has been added to your wishlist." }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Remove an item from the Wishlist using the product ID
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns>JsonResult response text</returns>
        public JsonResult removeFromWishlist(int iProductID)
        {
            if (Request.Cookies["WishList"] != null)
            {
                // Your cookie exists - grab your value and create your List
                List<int> productWishList = Request.Cookies["WishList"].Value.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                var productToRemove = productWishList.FindIndex(x => x == iProductID);
                if(productToRemove >= 0)
                {
                    productWishList.RemoveAt(productToRemove);
                }          
                if(productWishList.Count == 0)
                {
                    HttpCookie currentUserCookie = System.Web.HttpContext.Current.Request.Cookies["WishList"];
                    System.Web.HttpContext.Current.Response.Cookies.Remove("WishList");
                    currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                    currentUserCookie.Value = null;
                    System.Web.HttpContext.Current.Response.SetCookie(currentUserCookie);
                }
                else
                {
                    var WishListString = String.Join(",", productWishList);
                    System.Web.HttpContext.Current.Response.Cookies.Remove("WishList");
                    HttpCookie wishListCookie = new HttpCookie("WishList", WishListString);
                    // The cookie will exist for 7 days
                    wishListCookie.Expires = DateTime.Now.AddDays(30);

                    // Write the Cookie to your Response
                    Response.Cookies.Add(wishListCookie);
                }
            }
            return Json(new { success = true, responseText = "The product has been removed from the wishlist." }, JsonRequestBehavior.AllowGet);
        }
    }
}