using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Colmart.Models;
using Colmart.Model_Manager;
using Colmart.View_Models;
using PagedList;

namespace Colmart.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        /// <summary>
        /// Gets the Shop to display for UI
        /// </summary>
        /// <param name="page"></param>
        /// <param name="strPageHeader">Page header</param>
        /// <param name="iPageSize"></param>
        /// <param name="strCategories">Filtering property</param>
        /// <returns>ActionResult with viewbag</returns>
        public ActionResult Shop(int? page, string strPageHeader, int? iPageSize, string strCategories)
        {
            List<clsProducts> clsProductsList = new List<clsProducts>();
            var clsProductsManager = new clsProductsManager();
            var clsProductCategoriesManager = new clsProductCategoriesManager();

            int pageSize;
            ViewBag.SelectedCategories = strCategories;

            if (iPageSize != null)
                pageSize = Convert.ToInt32(iPageSize);
            else
                pageSize = 30;

            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            if (strCategories != null)
            {
                int[] iCategories = strCategories.Split(',').Select(int.Parse).ToArray();
                foreach(var category in iCategories)
                {
                    List<clsProducts> selectedCategoriesList = new List<clsProducts>();
                    selectedCategoriesList = clsProductsManager.getAllProductsByCategoryID(category);
                    clsProductsList.AddRange(selectedCategoriesList);
                }
            }
            else
            {
                ViewBag.SelectedCategories = 0;
                clsProductsList = clsProductsManager.getAllProductsList();
            }

            List<clsProductCategories> clsProductCategoriesList = clsProductCategoriesManager.getAllProductCategoriesList();
            clsProductCategoriesList = clsProductCategoriesList.OrderBy(x => x.strTitle).ToList();
            IPagedList<clsProducts> productsList = null;
            productsList = clsProductsList.ToPagedList(pageIndex, pageSize);
            #region ViewBags
            if (Session["clsUser"] != null)
                ViewBag.Layout = "_ColmartLayout.cshtml";
            else
                ViewBag.Layout = "_RolandoLayout.cshtml";
            ViewBag.iProductCount = pageSize;
            ViewBag.Categories = clsProductCategoriesList;
            ViewBag.Products = clsProductsList.Count();
            ViewBag.Message = "Your application description page.";
            ViewBag.Header = strPageHeader;
            #endregion
            return View(productsList);
        }

        /// <summary>
        /// RecentlyViewed item List generation
        /// </summary>
        /// <returns>ActionResult as PartialView</returns>
        public ActionResult _RecentlyViewed()
        {
            clsProductsManager clsProductsManager = new clsProductsManager();
            List<clsProducts> recentlyViewedProducts = new List<clsProducts>();
            clsRecentlyViewed clsRecentlyViewed = new clsRecentlyViewed();
            if (Request.Cookies["RecentlyViewed"] != null)
            {
                clsRecentlyViewed.bRecentlyViewed = true;
                // Your cookie exists - grab your value and create your List
                List<int> productWishList = Request.Cookies["RecentlyViewed"].Value.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                var iCount = 0;
                foreach (var product in productWishList)
                {
                    iCount++;
                    {
                        clsProducts clsRecentlyViewedProducts = new clsProducts();
                        clsRecentlyViewedProducts = clsProductsManager.getProductByID(product);
                        recentlyViewedProducts.Add(clsRecentlyViewedProducts);
                    }
                    if (iCount == 3)
                    {
                        break;
                    }
                }
                clsRecentlyViewed.lstRecentlyViewed = recentlyViewedProducts;
            }
            else
            {
                clsRecentlyViewed.bRecentlyViewed = false;
            }
            return PartialView(clsRecentlyViewed);
        }

        /// <summary>
        /// When the amount of products change
        /// </summary>
        /// <param name="iPageSize">Product list int</param>
        /// <returns>JsonResult redirectURL</returns>
        public JsonResult changeProductCount(int? iPageSize)
        {
            return Json(new { redirectURL = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult changeProductFilters()
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}