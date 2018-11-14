using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColmartCMS.View_Models;
using Colmart.Model_Manager;
using Colmart.Models;
using ColmartCMS.View_Models.ProductAssociation;

namespace ColmartCMS.Controllers
{
    public class ProductAssociationController : Controller
    {
        // GET: CMS/ProductAssociation
        public ActionResult ProductAssociation()
        {
            clsAssociationManager clsAssociationManager = new clsAssociationManager();
            clsProductsManager clsProductsManager = new clsProductsManager();
            clsProductAssociationAdd clsProductAssociationAdd = new clsProductAssociationAdd();
            clsProductAssociationAdd.lstMainProducts = clsProductsManager.getAllProductsList();
            clsProductAssociationAdd.lstMainProducts = clsProductAssociationAdd.lstMainProducts.OrderBy(m => m.strTitle);
            clsProductAssociationAdd.lstAssociatedProducts = clsProductsManager.getAllProductsList();
            clsProductAssociationAdd.lstAssociatedProducts = clsProductAssociationAdd.lstAssociatedProducts.OrderBy(m => m.strTitle);
            clsProductAssociationAdd.lstAssociations = clsAssociationManager.getAllAssociationsList();
            clsProductAssociationAdd.lstAssociations = clsProductAssociationAdd.lstAssociations.OrderBy(m => m.strTitle);
            return View(clsProductAssociationAdd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductAssociation(clsProductAssociationAdd clsProductAssociationAdd)
        {
            clsAssociationManager clsAssociationManager = new clsAssociationManager();
            clsProductsManager clsProductsManager = new clsProductsManager();
            clsProducts clsMainProduct = new clsProducts();
            clsProducts clsAssociatedProduct = new clsProducts();
            List<clsProducts> lstProducts = new List<clsProducts>();
            List<clsProducts> lstAssociatedProducts = new List<clsProducts>();

            clsMainProduct = clsProductsManager.getProductByStyleCode(clsProductAssociationAdd.clsMainProducts.strStyleCode);
            string strMainStyleCode = clsMainProduct.strStyleCode.Substring(0, 11);

            clsAssociatedProduct = clsProductsManager.getProductByStyleCode(clsProductAssociationAdd.clsAssociatedProducts.strStyleCode);
            string strAssociatedStyleCode = clsAssociatedProduct.strStyleCode.Substring(0, 11);

            lstProducts = clsProductsManager.getAllProductsByStyleCode(strMainStyleCode);
            lstAssociatedProducts = clsProductsManager.getAllProductsByStyleCode(strAssociatedStyleCode);

            bool bDoesAssociationExist = clsAssociationManager.checkIfSpecificProductAssociationExists(strMainStyleCode, strAssociatedStyleCode);
            if(bDoesAssociationExist)
            {

            }
            else
            {
                clsAssociationManager.saveProductAssociation(clsProductAssociationAdd.clsProductAssociations);
            }
      
            return RedirectToAction("ProductsView", "Products");
        }
    }
}