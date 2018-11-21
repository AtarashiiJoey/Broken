using Colmart;
using Colmart.Model_Manager;
using Colmart.Models;
using ColmartCMS.View_Models.ProductSubCategories;
using System.Linq;
using System.Web.Mvc;

namespace ColmartCMS.Controllers
{
    public class ProductSubCategoriesController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();

        // GET: ProductSubCategories
        public ActionResult ProductSubCategoriesView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductSubCategoriesView clsProductSubCategoriesView = new clsProductSubCategoriesView();
            clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();
            clsProductSubCategoriesView.lstProductSubCategories = clsProductSubCategoriesManager.getAllProductSubCategoriesOnlyList();

            return View(clsProductSubCategoriesView);
        }

        public ActionResult ProductSubCategoryAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductSubCategories clsProductSubCategory = new clsProductSubCategories();

            return View(clsProductSubCategory);
        }

        //Add CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductSubCategoryAdd(clsProductSubCategories clsProductSubCategory)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();
            clsProductSubCategoriesManager.saveProductSubCategory(clsProductSubCategory);

            //Add successful / notification
            TempData["bIsProductSubCategoryAdded"] = true;

            return RedirectToAction("ProductSubCategoriesView", "ProductSubCategories");
        }

        //Edit CMS Page
        public ActionResult ProductSubCategoryEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("ProductSubCategoriesView", "ProductSubCategories");
            }

            clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();
            clsProductSubCategories clsProductSubCategory = clsProductSubCategoriesManager.getProductSubCategoryByID(id);

            return View(clsProductSubCategory);
        }

        //Edit CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductSubCategoryEdit(clsProductSubCategories clsProductSubCategory)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();
            clsProductSubCategories clsExistingProductSubCategory = clsProductSubCategoriesManager.getProductSubCategoryByID(clsProductSubCategory.iProductSubCategoryID);

            clsExistingProductSubCategory.strTitle = clsProductSubCategory.strTitle;
            clsProductSubCategoriesManager.saveProductSubCategory(clsExistingProductSubCategory);

            //Add successful / notification
            TempData["bIsProductSubCategoryUpdated"] = true;

            return RedirectToAction("ProductSubCategoriesView", "ProductSubCategories");
        }

        //Delete Page
        [HttpPost]
        public ActionResult ProductSubCategoryDelete(int iProductSubCategoryID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iProductSubCategoryID == 0)
            {
                return RedirectToAction("ProductSubCategoriesView", "ProductSubCategories");
            }

            clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();
            clsProductSubCategoriesManager.removeProductSubCategoryByID(iProductSubCategoryID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsProductSubCategoryDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Check if exists
        [HttpPost]
        public JsonResult checkIfProductSubCategoryExists(string strTitle)
        {
            bool bCanUseTitle = false;
            bool bExists = db.tblProductSubCategories.Any(ProductSubCategory => ProductSubCategory.strTitle.ToLower() == strTitle.ToLower() && ProductSubCategory.bIsDeleted == false);

            if (bExists == false)
                bCanUseTitle = true;

            return Json(bCanUseTitle, JsonRequestBehavior.AllowGet);
        }
    }
}