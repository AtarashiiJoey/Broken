using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.Models;
using ColmartCMS.View_Models.ProductSizes;
using Colmart.Model_Manager;
using Colmart;

namespace ColmartCMS.Controllers
{
    public class ProductSizesController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();

        // GET: ProductSizes
        public ActionResult ProductSizesView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductSizesView clsProductSizesView = new clsProductSizesView();
            clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();
            clsProductSizesView.lstProductSizes = clsProductSizesManager.getAllProductSizesList();

            return View(clsProductSizesView);
        }

        public ActionResult ProductSizeAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductsManager clsProductsManager = new clsProductsManager();

            clsProductSizeAdd clsProductSizeAdd = new clsProductSizeAdd();
            clsProductSizeAdd.lstProducts = clsProductsManager.getAllProductsOnlyList();

            return View(clsProductSizeAdd);
        }

        //Add CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductSizeAdd(clsProductSizeAdd clsProductSizeAdd)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();
            clsProductSizesManager.saveProductSize(clsProductSizeAdd.clsProductSize);

            //Add successful / notification
            TempData["bIsProductSizeAdded"] = true;

            return RedirectToAction("ProductSizesView", "ProductSizes");
        }

        //Edit CMS Page
        public ActionResult ProductSizeEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("ProductSizesView", "ProductSizes");
            }

            clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();

            clsProductSizeEdit clsProductSizeEdit = new clsProductSizeEdit();
            clsProductSizeEdit.clsProductSize = clsProductSizesManager.getProductSizeByID(id);

            return View(clsProductSizeEdit);
        }

        //Edit CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductSizeEdit(clsProductSizeEdit clsProductSizeEdit)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();
            clsProductSizes clsExistingProductSize = clsProductSizesManager.getProductSizeByID(clsProductSizeEdit.clsProductSize.iProductSizeID);

            clsExistingProductSize.strSize = clsProductSizeEdit.clsProductSize.strSize;
            clsExistingProductSize.dblPrice = clsProductSizeEdit.clsProductSize.dblPrice;
            clsExistingProductSize.iQuantityAvailable = clsProductSizeEdit.clsProductSize.iQuantityAvailable;
            clsExistingProductSize.iProductID = clsProductSizeEdit.clsProductSize.iProductID;

            clsProductSizesManager.saveProductSize(clsExistingProductSize);

            //Add successful / notification
            TempData["bIsProductSizeUpdated"] = true;

            return RedirectToAction("ProductSizesView", "ProductSizes");
        }

        //Delete Page
        [HttpPost]
        public ActionResult ProductSizeDelete(int iProductSizeID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iProductSizeID == 0)
            {
                return RedirectToAction("ProductSizesView", "ProductSizes");
            }

            clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();
            clsProductSizesManager.removeProductSizeByID(iProductSizeID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsProductSizeDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Check if exists
        [HttpPost]
        public JsonResult checkIfProductSizeExists(string strSize)
        {
            bool bCanUseTitle = false;
            bool bExists = db.tblProductSizes.Any(ProductSize => ProductSize.strSize.ToLower() == strSize.ToLower() && ProductSize.bIsDeleted == false);

            if (bExists == false)
                bCanUseTitle = true;

            return Json(bCanUseTitle, JsonRequestBehavior.AllowGet);
        }
    }
}