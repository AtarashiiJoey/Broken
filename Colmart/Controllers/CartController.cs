using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using Colmart.View_Models;
using Colmart.Model_Manager;

namespace Colmart.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            if (Session["clsUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                clsProductsManager clsProductsManager = new clsProductsManager();
                List<clsCart> CartProducts = new List<clsCart>();
                clsCartView clsCartView = new clsCartView();
                double dblCartTotal = 0;

                if (Session["CartItems"] != null)
                {
                    // Your cookie exists - grab your value and create your List
                    List<clsCartItems> productCart = Session["CartItems"] as List<clsCartItems>;
                    List<int> lstProductIDs = new List<int>();
                    foreach (var item in productCart)
                    {
                        if (!lstProductIDs.Contains(item.iProductID))
                        {
                            lstProductIDs.Add(item.iProductID);
                            clsCart clsCartProduct = new clsCart();
                            clsCartProduct = clsProductsManager.getCartProductByID(item.iProductID);
                            foreach (var size in productCart.Where(x => x.iProductID == item.iProductID))
                            {
                                foreach (var items in clsCartProduct.lstProductSizes)
                                {
                                    if (items.iProductSizeID == size.iCartProductID)
                                    {
                                        items.iQuantityOrderded = size.iProductValue;
                                        items.dblSubTotal = size.iProductValue * items.dblPrice;
                                        dblCartTotal = dblCartTotal + items.dblSubTotal;
                                    }
                                }
                            }
                            ViewBag.cartTotal = dblCartTotal;
                            CartProducts.Add(clsCartProduct);
                        }

                    }
                }
                //}
                return View(CartProducts);
            }
        }

        //Adding wishlist product to cookie
        public JsonResult addToCart(params clsCartItems[] cartItems)
        {
            if (cartItems == null)
            {
                return Json(new { success = false, message = "Please select the sizes you would like to add to your cart!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (Session["CartItems"] != null)
                {
                    // Your cookie exists - grab your value and create your List
                    List<clsCartItems> productCart = Session["CartItems"] as List<clsCartItems>;
                    foreach (var item in cartItems)
                    {
                        if (productCart.Exists(x => x.iCartProductID == item.iCartProductID))
                        {
                            clsCartItems clsCartItem = productCart.Find(x => x.iCartProductID == item.iCartProductID);
                            if (clsCartItem.iProductValue + item.iProductValue > item.iStockAvailable)
                            {
                                clsCartItem.iProductValue = item.iStockAvailable;
                            }
                            else
                            {
                                clsCartItem.iProductValue = clsCartItem.iProductValue + item.iProductValue;
                            }
                        }
                        else
                        {
                            productCart.Add(item);
                        }
                    }
                    Session["CartItems"] = null;
                    Session["CartItems"] = productCart;
                }
                else
                {
                    // Build your list
                    List<clsCartItems> productCart = new List<clsCartItems>();
                    foreach (var item in cartItems)
                    {
                        productCart.Add(item);
                    }
                    Session["CartItems"] = productCart;
                }
                return Json(new { success = true, message = "The product has been added to your cart." }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult addToWishlist(params clsCartItems[] cartItems)
        {
            if (cartItems == null)
            {
                return Json(new { success = false, message = "Please select the sizes you would like to add to your cart!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (Request.Cookies["WishList"] != null)
                {
                    // Your cookie exists - grab your value and create your List
                    List<int> productWishList = Request.Cookies["WishList"].Value.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                    foreach (var item in cartItems)
                    {
                        if (productWishList.Contains(item.iProductID))
                        {

                        }
                        else
                        {
                            productWishList.Add(item.iProductID);
                        }
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
                    foreach (var item in cartItems)
                    {
                        if (productWishList.Contains(item.iProductID))
                        {

                        }
                        else
                        {
                            productWishList.Add(item.iProductID);
                        }
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
        }

        //Adding wishlist product to cookie
        public JsonResult updateCart(params clsCartItems[] cartItems)
        {
            if (Session["CartItems"] != null)
            {
                // Your cookie exists - grab your value and create your List
                List<clsCartItems> productCart = Session["CartItems"] as List<clsCartItems>;
                foreach (var item in cartItems)
                {
                    if (productCart.Exists(x => x.iCartProductID == item.iCartProductID))
                    {
                        clsCartItems clsCartItem = productCart.Find(x => x.iCartProductID == item.iCartProductID);
                        clsCartItem.iProductValue = item.iProductValue;
                        if (clsCartItem.iProductValue > item.iStockAvailable)
                        {
                            clsCartItem.iProductValue = item.iStockAvailable;
                        }
                    }
                    else
                    {
                        if (item.iProductValue > item.iStockAvailable)
                            item.iProductValue = item.iStockAvailable;
                        productCart.Add(item);
                    }
                }
                Session["CartItems"] = null;
                Session["CartItems"] = productCart;
            }
            else
            {
                // Build your list
                List<clsCartItems> productCart = new List<clsCartItems>();
                foreach (var item in cartItems)
                {
                    productCart.Add(item);
                }
                Session["CartItems"] = productCart;
            }
            List<clsCartItems> productData = Session["CartItems"] as List<clsCartItems>;
            return Json(new { success = true, responseText = "The product has been added to the wishlist.", productData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult removeFromCart(int iProductID)
        {
            if (Request.Cookies["CartItems"] != null)
            {
                // Your cookie exists - grab your value and create your List
                List<int> productCart = Request.Cookies["Cart"].Value.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                var productToRemove = productCart.FindIndex(x => x == iProductID);
                if (productToRemove >= 0)
                {
                    productCart.RemoveAt(productToRemove);
                }
                if (productCart.Count == 0)
                {
                    HttpCookie currentCartCookie = System.Web.HttpContext.Current.Request.Cookies["Cart"];
                    System.Web.HttpContext.Current.Response.Cookies.Remove("Cart");
                    currentCartCookie.Expires = DateTime.Now.AddDays(-10);
                    currentCartCookie.Value = null;
                    System.Web.HttpContext.Current.Response.SetCookie(currentCartCookie);
                }
                else
                {
                    var WishListString = String.Join(",", productCart);
                    System.Web.HttpContext.Current.Response.Cookies.Remove("Cart");
                    HttpCookie CartCookie = new HttpCookie("Cart", WishListString);
                    // The cookie will exist for 7 days
                    CartCookie.Expires = DateTime.Now.AddDays(30);
                    // Write the Cookie to your Response
                    Response.Cookies.Add(CartCookie);
                }
            }
            return Json(new { success = true, responseText = "The product has been removed from the cart." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult clearCart()
        {
            if (Session["CartItems"] != null)
                Session["CartItems"] = null;

            return Json(new { success = true, responseText = "The cart has been cleared." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuickCart()
        {
            clsProductsManager clsProductsManager = new clsProductsManager();
            List<clsCart> CartProducts = new List<clsCart>();
            clsCartView clsCartView = new clsCartView();

            if (Session["CartItems"] != null)
            {
                // Your cookie exists - grab your value and create your List
                List<clsCartItems> productCart = Session["CartItems"] as List<clsCartItems>;
                List<int> lstProductIDs = new List<int>();
                foreach (var item in productCart)
                {
                    if (!lstProductIDs.Contains(item.iProductID))
                    {
                        lstProductIDs.Add(item.iProductID);
                        clsCart clsCartProduct = new clsCart();
                        clsCartProduct = clsProductsManager.getCartProductByID(item.iProductID);
                        //foreach (var size in productCart.Where(x => x.iProductID == item.iProductID))
                        //{
                        //    foreach (var items in clsCartProduct.lstProductSizes)
                        //    {
                        //        if (items.iProductSizeID == size.iCartProductID)
                        //        {
                        //            items.iQuantityOrderded = size.iProductValue;
                        //            items.dblSubTotal = size.iProductValue * items.dblPrice;
                        //        }
                        //    }
                        //}
                        CartProducts.Add(clsCartProduct);
                    }

                }
            }
            clsCartView.lstCart = CartProducts;
            return PartialView(CartProducts);
        }

        public ActionResult _CartBadge()
        {
            List<clsCartItems> productCart = Session["CartItems"] as List<clsCartItems>;
            var clsCartBadge = new clsCartBadge();
            if(productCart != null)
            {
                clsCartBadge.iCartCount = productCart.Count;
            }
            else
            {
                clsCartBadge.iCartCount = 0;
            }
            return PartialView(clsCartBadge);
        }
    }
}
