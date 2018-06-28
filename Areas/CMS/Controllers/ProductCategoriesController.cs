using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using ColmartCMS.View_Models.ProductCategories;
using Colmart.Model_Manager;
using Colmart;

namespace ColmartCMS.Controllers
{
    public class ProductCategoriesController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();

        // GET: ProductCategories
        public ActionResult ProductCategoriesView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductCategoriesView clsProductCategoriesView = new clsProductCategoriesView();
            clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
            clsProductCategoriesView.lstProductCategories = clsProductCategoriesManager.getAllProductCategoriesOnlyList();

            return View(clsProductCategoriesView);
        }

        public ActionResult ProductCategoryAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductCategories clsProductCategory = new clsProductCategories();

            return View(clsProductCategory);
        }

        //Add CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCategoryAdd(clsProductCategories clsProductCategory)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
            clsProductCategoriesManager.saveProductCategory(clsProductCategory);

            //Add successful / notification
            TempData["bIsProductCategoryAdded"] = true;

            return RedirectToAction("ProductCategoriesView", "ProductCategories");
        }

        //Edit CMS Page
        public ActionResult ProductCategoryEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("ProductCategoriesView", "ProductCategories");
            }

            clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
            clsProductCategories clsProductCategory = clsProductCategoriesManager.getProductCategoryByID(id);

            return View(clsProductCategory);
        }

        //Edit CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCategoryEdit(clsProductCategories clsProductCategory)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
            clsProductCategories clsExistingProductCategory = clsProductCategoriesManager.getProductCategoryByID(clsProductCategory.iProductCategoryID);

            clsExistingProductCategory.strTitle = clsProductCategory.strTitle;
            clsProductCategoriesManager.saveProductCategory(clsExistingProductCategory);

            //Add successful / notification
            TempData["bIsProductCategoryUpdated"] = true;

            return RedirectToAction("ProductCategoriesView", "ProductCategories");
        }

        //Delete Page
        [HttpPost]
        public ActionResult ProductCategoryDelete(int iProductCategoryID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iProductCategoryID == 0)
            {
                return RedirectToAction("ProductCategoriesView", "ProductCategories");
            }

            clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
            clsProductCategoriesManager.removeProductCategoryByID(iProductCategoryID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsProductCategoryDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Check if exists
        [HttpPost]
        public JsonResult checkIfProductCategoryExists(string strTitle)
        {
            bool bCanUseTitle = false;
            bool bExists = db.tblProductCategories.Any(ProductCategory => ProductCategory.strTitle.ToLower() == strTitle.ToLower() && ProductCategory.bIsDeleted == false);

            if (bExists == false)
                bCanUseTitle = true;

            return Json(bCanUseTitle, JsonRequestBehavior.AllowGet);
        }
    }
}