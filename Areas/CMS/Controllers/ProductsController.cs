using Colmart;
using Colmart.Model_Manager;
using Colmart.Models;
using ColmartCMS.View_Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;

namespace ColmartCMS.Controllers
{
    public class ProductsController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();

        // GET: Products
        public ActionResult ProductsView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductsView clsProductsView = new clsProductsView();
            clsProductsManager clsProductsManager = new clsProductsManager();
            clsProductsView.lstProducts = clsProductsManager.getAllProductsList();

            return View(clsProductsView);
        }

        public ActionResult ProductAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
            clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();

            clsProductAdd clsProductAdd = new clsProductAdd();
            clsProductAdd.lstProductCategories = clsProductCategoriesManager.getAllProductCategoriesOnlyList();
            clsProductAdd.lstProductSubCategories = clsProductSubCategoriesManager.getAllProductSubCategoriesOnlyList();

            return View(clsProductAdd);
        }

        //Add CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductAdd(clsProductAdd clsProductAdd)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductsManager clsProductsManager = new clsProductsManager();
            clsProductsManager.saveProduct(clsProductAdd.clsProduct);

            //Add successful / notification
            TempData["bIsProductAdded"] = true;

            return RedirectToAction("ProductsView", "Products");
        }

        //Edit CMS Page
        public ActionResult ProductEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("ProductsView", "Products");
            }

            clsProductsManager clsProductsManager = new clsProductsManager();
            clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
            clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();

            clsProductEdit clsProductEdit = new clsProductEdit();
            clsProductEdit.clsProduct = clsProductsManager.getProductByID(id);
            clsProductEdit.lstProductCategories = clsProductCategoriesManager.getAllProductCategoriesOnlyList();
            clsProductEdit.lstProductSubCategories = clsProductSubCategoriesManager.getAllProductSubCategoriesOnlyList();

            return View(clsProductEdit);
        }

        //Edit CMS Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit(clsProductEdit clsProductEdit)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsProductsManager clsProductsManager = new clsProductsManager();
            clsProducts clsExistingProduct = clsProductsManager.getProductByID(clsProductEdit.clsProduct.iProductID);

            clsExistingProduct.strTitle = clsProductEdit.clsProduct.strTitle;
            clsExistingProduct.strStyleCode = clsProductEdit.clsProduct.strStyleCode;
            clsExistingProduct.strProductColour = clsProductEdit.clsProduct.strProductColour;
            clsExistingProduct.strFullDescription = clsProductEdit.clsProduct.strFullDescription;
            clsExistingProduct.strImageURL = clsProductEdit.clsProduct.strImageURL;

            clsExistingProduct.iProductCategoryID = clsProductEdit.clsProduct.iProductCategoryID;
            clsExistingProduct.iProductSubCategoryID = clsProductEdit.clsProduct.iProductSubCategoryID;

            clsProductsManager.saveProduct(clsExistingProduct);

            //Add successful / notification
            TempData["bIsProductUpdated"] = true;

            return RedirectToAction("ProductsView", "Products");
        }

        //Delete Page
        [HttpPost]
        public ActionResult ProductDelete(int iProductID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iProductID == 0)
            {
                return RedirectToAction("ProductsView", "Products");
            }

            clsProductsManager clsProductsManager = new clsProductsManager();
            clsProductsManager.removeProductByID(iProductID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsProductDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Import Products
        [HttpPost]
        public async Task<ActionResult> ProductsImport()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            using (var client = new HttpClient())
            {
                try
                {
                    //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    string url = "http://colmart.co.za/GenerateStockAvailableXML.php";
                    var responseString = await client.GetStringAsync(url);

                    XmlDocument responseDoc = new XmlDocument();
                    responseDoc.LoadXml(responseString);

                    XmlNodeList productList = responseDoc.GetElementsByTagName("Product");
                    if (productList.Count > 0)
                    {
                        clsProductsManager clsProductsManager = new clsProductsManager();
                        clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
                        clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();
                        clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();

                        List<clsProducts> lstProducts = clsProductsManager.getAllProductsOnlyList();
                        List<clsProductCategories> lstProductCategories = clsProductCategoriesManager.getAllProductCategoriesOnlyList();
                        List<clsProductSubCategories> lstProductSubCategories = clsProductSubCategoriesManager.getAllProductSubCategoriesOnlyList();

                        foreach (XmlNode xmlnProduct in productList)
                        {
                            //Get Product description and style code 
                            XmlNodeList xmlnProductDesc = xmlnProduct.SelectNodes("ProductDesc");
                            XmlNodeList xmlnStyleCode = xmlnProduct.SelectNodes("StyleCode");
                            if (xmlnProductDesc.Count > 0 && xmlnStyleCode.Count > 0)
                            {
                                if (lstProducts.Any(Product => Product.strStyleCode.ToLower() == xmlnStyleCode[0].InnerText.ToLower() && Product.bIsDeleted == false))
                                    continue;

                                clsProducts clsProduct = new clsProducts();

                                XmlNodeList xmlnProductName = xmlnProduct.SelectNodes("ProductName");
                                XmlNodeList xmlnFullDesc = xmlnProduct.SelectNodes("FullDesc");
                                XmlNodeList xmlnImageURL = xmlnProduct.SelectNodes("ImageURL");
                                XmlNodeList xmlnProductCategory = xmlnProduct.SelectNodes("ProductCategory");
                                XmlNodeList xmlnProductSubCategory = xmlnProduct.SelectNodes("ProductSubCategory");

                                clsProduct.strTitle = xmlnProductDesc[0].InnerText;
                                clsProduct.strStyleCode = xmlnStyleCode[0].InnerText;
                                if (xmlnProductName.Count > 0)
                                    clsProduct.strProductColour = xmlnProductName[0].InnerText;
                                else
                                    clsProduct.strProductColour = "OTHER";

                                if (xmlnFullDesc.Count > 0)
                                    clsProduct.strFullDescription = xmlnFullDesc[0].InnerText;
                                else
                                    clsProduct.strFullDescription = "";

                                if (xmlnImageURL.Count > 0)
                                    clsProduct.strImageURL = xmlnImageURL[0].InnerText;
                                else
                                    clsProduct.strImageURL = "";

                                if (xmlnProductCategory.Count > 0)
                                    clsProduct.iProductCategoryID = getProductCategoryID(xmlnProductCategory[0].InnerText);
                                else
                                    clsProduct.iProductCategoryID = getProductCategoryID("OTHER");

                                if (xmlnProductSubCategory.Count > 0)
                                    clsProduct.iProductSubCategoryID = getProductSubCategoryID(xmlnProductSubCategory[0].InnerText);
                                else
                                    clsProduct.iProductSubCategoryID = getProductSubCategoryID("OTHER");

                                //Save Product
                                int iProductID = clsProductsManager.saveProductAndGetID(clsProduct);

                                //Get product sizes
                                XmlNodeList xmlnProductSizes = xmlnProduct.SelectNodes("Size");
                                if (xmlnProductSizes.Count > 0)
                                {
                                    clsProductSizes clsProductSizes = new clsProductSizes();

                                    foreach (XmlNode xmlnSize in xmlnProductSizes)
                                    {
                                        XmlNodeList xmlnSizeName = xmlnSize.SelectNodes("SizeName");
                                        XmlNodeList xmlnPrice = xmlnSize.SelectNodes("Price");
                                        XmlNodeList xmlnQtyAvailable = xmlnSize.SelectNodes("QtyAvailable");

                                        clsProductSizes.iProductID = iProductID;
                                        if (xmlnSizeName.Count > 0)
                                            clsProductSizes.strSize = xmlnSizeName[0].InnerText;
                                        if (xmlnPrice.Count > 0)
                                        {
                                            string strPrice = "";
                                            if (xmlnPrice[0].InnerText.Contains("."))
                                                strPrice = xmlnPrice[0].InnerText.Replace('.', ',');
                                            else
                                                strPrice = xmlnPrice[0].InnerText;

                                            clsProductSizes.dblPrice = Convert.ToDouble(strPrice);
                                        }
                                        if (xmlnQtyAvailable.Count > 0)
                                            clsProductSizes.iQuantityAvailable = Convert.ToInt32(xmlnQtyAvailable[0].InnerText);

                                        clsProductSizesManager.saveProductSize(clsProductSizes);
                                    }
                                }
                            }
                        }
                    }

                    bIsSuccess = true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

            //Import successful / notification
            if (bIsSuccess)
                TempData["bIsProductsImported"] = true;
            else
                TempData["bIsProductsImported"] = false;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Check if exists
        [HttpPost]
        public JsonResult checkIfProductExists(string strTitle)
        {
            bool bCanUseTitle = false;
            bool bExists = db.tblProducts.Any(Product => Product.strTitle.ToLower() == strTitle.ToLower() && Product.bIsDeleted == false);

            if (bExists == false)
                bCanUseTitle = true;

            return Json(bCanUseTitle, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult checkIfStyleCodeExists([Bind(Prefix = "clsProduct.strStyleCode")] string strStyleCode)
        {
            bool bCanUseTitle = false;
            bool bExists = db.tblProducts.Any(Product => Product.strStyleCode.ToLower() == strStyleCode.ToLower() && Product.bIsDeleted == false);

            if (bExists == false)
                bCanUseTitle = true;

            return Json(bCanUseTitle, JsonRequestBehavior.AllowGet);
        }

        private int getProductCategoryID(string strTitle)
        {
            int iProductCategoryID = 0;
            clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
            List<clsProductCategories> lstProductCategories = clsProductCategoriesManager.getAllProductCategoriesOnlyList();

            clsProductCategories clsProductCategory = lstProductCategories.FirstOrDefault(productCategory => productCategory.strTitle.Contains(strTitle) && productCategory.bIsDeleted == false);
            if (clsProductCategory != null)
            {
                iProductCategoryID = clsProductCategory.iProductCategoryID;
            }
            else
            {
                clsProductCategories clsProductCategoryNew = new clsProductCategories();
                clsProductCategoryNew.strTitle = strTitle;
                iProductCategoryID = clsProductCategoriesManager.saveProductCategoryAndGetID(clsProductCategoryNew);
            }

            return iProductCategoryID;
        }

        private int getProductSubCategoryID(string strTitle)
        {
            int iProductSubCategoryID = 0;
            clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();
            List<clsProductSubCategories> lstProductSubCategories = clsProductSubCategoriesManager.getAllProductSubCategoriesOnlyList();

            clsProductSubCategories clsProductSubCategory = lstProductSubCategories.FirstOrDefault(productSubCategory => productSubCategory.strTitle.Contains(strTitle) && productSubCategory.bIsDeleted == false);
            if (clsProductSubCategory != null)
            {
                iProductSubCategoryID = clsProductSubCategory.iProductSubCategoryID;
            }
            else
            {
                clsProductSubCategories clsProductSubCategoryNew = new clsProductSubCategories();
                clsProductSubCategoryNew.strTitle = strTitle;
                iProductSubCategoryID = clsProductSubCategoriesManager.saveProductSubCategoryAndGetID(clsProductSubCategoryNew);
            }

            return iProductSubCategoryID;
        }
    }
}