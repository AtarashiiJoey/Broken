﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Colmart;
using Colmart.Models;

namespace Colmart.Model_Manager
{
    public class clsProductsManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsProducts> getAllProductsList()
        {
            List<clsProducts> lstProducts = new List<clsProducts>();
            var lstGetProductsList = db.tblProducts.Where(Product => Product.bIsDeleted == false).ToList();

            if (lstGetProductsList.Count > 0)
            {
                //Managers
                clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
                clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();
                clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();

                foreach (var item in lstGetProductsList)
                {
                    clsProducts clsProduct = new clsProducts();

                    clsProduct.iProductID = item.iProductID;
                    clsProduct.dtAdded = item.dtAdded;
                    clsProduct.iAddedBy = item.iAddedBy;
                    clsProduct.dtEdited = item.dtEdited;
                    clsProduct.iEditedBy = item.iEditedBy;

                    clsProduct.strTitle = item.strTitle;
                    clsProduct.strStyleCode = item.strStyleCode;
                    clsProduct.strProductColour = item.strProductColour;
                    clsProduct.strFullDescription = item.strFullDescription;
                    clsProduct.strImageURL = item.strImageURL;

                    clsProduct.iProductCategoryID = item.iProductCategoryID;
                    clsProduct.iProductSubCategoryID = item.iProductSubCategoryID;
                    clsProduct.bIsDeleted = item.bIsDeleted;

                    if (item.tblProductCategories != null)
                        clsProduct.clsProductCategory = clsProductCategoriesManager.convertProductCategoriesTableToClass(item.tblProductCategories);
                    if (item.tblProductSubCategories != null)
                        clsProduct.clsProductSubCategory = clsProductSubCategoriesManager.convertProductSubCategoriesTableToClass(item.tblProductSubCategories);

                    clsProduct.lstProductSizes = new List<clsProductSizes>();
                    if (item.tblProductSizes.Count > 0)
                    {
                        foreach (var ProductSizeItem in item.tblProductSizes)
                        {
                            clsProductSizes clsProductSize = clsProductSizesManager.convertProductSizesTableToClass(ProductSizeItem);
                            clsProduct.lstProductSizes.Add(clsProductSize);
                        }
                    }

                    lstProducts.Add(clsProduct);
                }
            }

            return lstProducts;
        }

        //Get All
        public List<clsProducts> getAllProductsOnlyList()
        {
            List<clsProducts> lstProducts = new List<clsProducts>();
            var lstGetProductsList = db.tblProducts.Where(Product => Product.bIsDeleted == false).ToList();

            if (lstGetProductsList.Count > 0)
            {
                //CMS User Access Manager
                clsCMSUserAccessManager clsCMSUserAccessManager = new clsCMSUserAccessManager();

                foreach (var item in lstGetProductsList)
                {
                    clsProducts clsProduct = new clsProducts();

                    clsProduct.iProductID = item.iProductID;
                    clsProduct.dtAdded = item.dtAdded;
                    clsProduct.iAddedBy = item.iAddedBy;
                    clsProduct.dtEdited = item.dtEdited;
                    clsProduct.iEditedBy = item.iEditedBy;

                    clsProduct.strTitle = item.strTitle;
                    clsProduct.strStyleCode = item.strStyleCode;
                    clsProduct.strProductColour = item.strProductColour;
                    clsProduct.strFullDescription = item.strFullDescription;
                    clsProduct.strImageURL = item.strImageURL;

                    clsProduct.iProductCategoryID = item.iProductCategoryID;
                    clsProduct.iProductSubCategoryID = item.iProductSubCategoryID;
                    clsProduct.bIsDeleted = item.bIsDeleted;

                    clsProduct.lstProductSizes = new List<clsProductSizes>();

                    lstProducts.Add(clsProduct);
                }
            }

            return lstProducts;
        }

        //Get
        public clsProducts getProductByID(int iProductID)
        {
            clsProducts clsProduct = null;
            tblProducts tblProducts = db.tblProducts.FirstOrDefault(Product => Product.iProductID == iProductID && Product.bIsDeleted == false);

            if (tblProducts != null)
            {
                //Managers
                clsProductCategoriesManager clsProductCategoriesManager = new clsProductCategoriesManager();
                clsProductSubCategoriesManager clsProductSubCategoriesManager = new clsProductSubCategoriesManager();
                clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();

                clsProduct = new clsProducts();

                clsProduct.iProductID = tblProducts.iProductID;
                clsProduct.dtAdded = tblProducts.dtAdded;
                clsProduct.iAddedBy = tblProducts.iAddedBy;
                clsProduct.dtEdited = tblProducts.dtEdited;
                clsProduct.iEditedBy = tblProducts.iEditedBy;

                clsProduct.strTitle = tblProducts.strTitle;
                clsProduct.strStyleCode = tblProducts.strStyleCode;
                clsProduct.strProductColour = tblProducts.strProductColour;
                clsProduct.strFullDescription = tblProducts.strFullDescription;
                clsProduct.strImageURL = tblProducts.strImageURL;

                clsProduct.iProductCategoryID = tblProducts.iProductCategoryID;
                clsProduct.iProductSubCategoryID = tblProducts.iProductSubCategoryID;
                clsProduct.bIsDeleted = tblProducts.bIsDeleted;

                if (tblProducts.tblProductCategories != null)
                    clsProduct.clsProductCategory = clsProductCategoriesManager.convertProductCategoriesTableToClass(tblProducts.tblProductCategories);
                if (tblProducts.tblProductSubCategories != null)
                    clsProduct.clsProductSubCategory = clsProductSubCategoriesManager.convertProductSubCategoriesTableToClass(tblProducts.tblProductSubCategories);

                clsProduct.lstProductSizes = new List<clsProductSizes>();
                if (tblProducts.tblProductSizes.Count > 0)
                {
                    foreach (var ProductSizeItem in tblProducts.tblProductSizes)
                    {
                        clsProductSizes clsProductSize = clsProductSizesManager.convertProductSizesTableToClass(ProductSizeItem);
                        clsProduct.lstProductSizes.Add(clsProductSize);
                    }
                }
            }

            return clsProduct;
        }

        //Save
        public void saveProduct(clsProducts clsProduct)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblProducts tblProducts = new tblProducts();

                tblProducts.iProductID = clsProduct.iProductID;

                tblProducts.strTitle = clsProduct.strTitle;
                tblProducts.strStyleCode = clsProduct.strStyleCode;
                tblProducts.strProductColour = clsProduct.strProductColour;
                tblProducts.strFullDescription = clsProduct.strFullDescription;
                tblProducts.strImageURL = clsProduct.strImageURL;

                tblProducts.iProductCategoryID = clsProduct.iProductCategoryID;
                tblProducts.iProductSubCategoryID = clsProduct.iProductSubCategoryID;
                tblProducts.bIsDeleted = clsProduct.bIsDeleted;

                //Add
                if (tblProducts.iProductID == 0)
                {
                    tblProducts.dtAdded = DateTime.Now;
                    tblProducts.iAddedBy = clsCMSUser.iCMSUserID;
                    tblProducts.dtEdited = DateTime.Now;
                    tblProducts.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblProducts.Add(tblProducts);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblProducts.dtAdded = clsProduct.dtAdded;
                    tblProducts.iAddedBy = clsProduct.iAddedBy;
                    tblProducts.dtEdited = DateTime.Now;
                    tblProducts.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblProducts>().AddOrUpdate(tblProducts);
                    db.SaveChanges();
                }
            }
        }

        //Save
        public int saveProductAndGetID(clsProducts clsProduct)
        {
            int iProductID = 0;
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblProducts tblProducts = new tblProducts();

                tblProducts.iProductID = clsProduct.iProductID;

                tblProducts.strTitle = clsProduct.strTitle;
                tblProducts.strStyleCode = clsProduct.strStyleCode;
                tblProducts.strProductColour = clsProduct.strProductColour;
                tblProducts.strFullDescription = clsProduct.strFullDescription;
                tblProducts.strImageURL = clsProduct.strImageURL;

                tblProducts.iProductCategoryID = clsProduct.iProductCategoryID;
                tblProducts.iProductSubCategoryID = clsProduct.iProductSubCategoryID;
                tblProducts.bIsDeleted = clsProduct.bIsDeleted;

                //Add
                if (tblProducts.iProductID == 0)
                {
                    tblProducts.dtAdded = DateTime.Now;
                    tblProducts.iAddedBy = clsCMSUser.iCMSUserID;
                    tblProducts.dtEdited = DateTime.Now;
                    tblProducts.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblProducts.Add(tblProducts);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblProducts.dtAdded = clsProduct.dtAdded;
                    tblProducts.iAddedBy = clsProduct.iAddedBy;
                    tblProducts.dtEdited = DateTime.Now;
                    tblProducts.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblProducts>().AddOrUpdate(tblProducts);
                    db.SaveChanges();
                }

                iProductID = tblProducts.iProductID;
            }

            return iProductID;
        }

        //Remove
        public void removeProductByID(int iProductID)
        {
            tblProducts tblProduct = db.tblProducts.Find(iProductID);
            if (tblProduct != null)
            {
                tblProduct.bIsDeleted = true;
                db.Entry(tblProduct).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check
        public bool checkIfProductExists(int iProductID)
        {
            bool bProductExists = db.tblProducts.Any(Product => Product.iProductID == iProductID && Product.bIsDeleted == false);
            return bProductExists;
        }

        //Convert database table to class
        public clsProducts convertProductsTableToClass(tblProducts tblProduct)
        {
            clsProducts clsProduct = new clsProducts();

            clsProduct.iProductID = tblProduct.iProductID;
            clsProduct.dtAdded = tblProduct.dtAdded;
            clsProduct.iAddedBy = tblProduct.iAddedBy;
            clsProduct.dtEdited = tblProduct.dtEdited;
            clsProduct.iEditedBy = tblProduct.iEditedBy;

            clsProduct.strTitle = tblProduct.strTitle;
            clsProduct.strStyleCode = tblProduct.strStyleCode;
            clsProduct.strProductColour = tblProduct.strProductColour;
            clsProduct.strFullDescription = tblProduct.strFullDescription;
            clsProduct.strImageURL = tblProduct.strImageURL;

            clsProduct.iProductCategoryID = tblProduct.iProductCategoryID;
            clsProduct.iProductSubCategoryID = tblProduct.iProductSubCategoryID;
            clsProduct.bIsDeleted = tblProduct.bIsDeleted;

            return clsProduct;
        }
    }
}
