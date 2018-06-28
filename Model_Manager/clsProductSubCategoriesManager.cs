using System;
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
    public class clsProductSubCategoriesManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsProductSubCategories> getAllProductSubCategoriesList()
        {
            List<clsProductSubCategories> lstProductSubCategories = new List<clsProductSubCategories>();
            var lstGetProductSubCategoriesList = db.tblProductSubCategories.Where(ProductSubCategory => ProductSubCategory.bIsDeleted == false).ToList();

            if (lstGetProductSubCategoriesList.Count > 0)
            {
                //Manager
                clsProductsManager clsProductsManager = new clsProductsManager();

                foreach (var item in lstGetProductSubCategoriesList)
                {
                    clsProductSubCategories clsProductSubCategory = new clsProductSubCategories();

                    clsProductSubCategory.iProductSubCategoryID = item.iProductSubCategoryID;
                    clsProductSubCategory.dtAdded = item.dtAdded;
                    clsProductSubCategory.iAddedBy = item.iAddedBy;
                    clsProductSubCategory.dtEdited = item.dtEdited;
                    clsProductSubCategory.iEditedBy = item.iEditedBy;

                    clsProductSubCategory.strTitle = item.strTitle;
                    clsProductSubCategory.bIsDeleted = item.bIsDeleted;

                    clsProductSubCategory.lstProducts = new List<clsProducts>();

                    if (item.tblProducts.Count > 0)
                    {
                        foreach (var ProductItem in item.tblProducts)
                        {
                            clsProducts clsProduct = clsProductsManager.convertProductsTableToClass(ProductItem);
                            clsProductSubCategory.lstProducts.Add(clsProduct);
                        }
                    }

                    lstProductSubCategories.Add(clsProductSubCategory);
                }
            }

            return lstProductSubCategories;
        }

        //Get All
        public List<clsProductSubCategories> getAllProductSubCategoriesOnlyList()
        {
            List<clsProductSubCategories> lstProductSubCategories = new List<clsProductSubCategories>();
            var lstGetProductSubCategoriesList = db.tblProductSubCategories.Where(ProductSubCategory => ProductSubCategory.bIsDeleted == false).ToList();

            if (lstGetProductSubCategoriesList.Count > 0)
            {
                foreach (var item in lstGetProductSubCategoriesList)
                {
                    clsProductSubCategories clsProductSubCategory = new clsProductSubCategories();

                    clsProductSubCategory.iProductSubCategoryID = item.iProductSubCategoryID;
                    clsProductSubCategory.dtAdded = item.dtAdded;
                    clsProductSubCategory.iAddedBy = item.iAddedBy;
                    clsProductSubCategory.dtEdited = item.dtEdited;
                    clsProductSubCategory.iEditedBy = item.iEditedBy;

                    clsProductSubCategory.strTitle = item.strTitle;
                    clsProductSubCategory.bIsDeleted = item.bIsDeleted;

                    clsProductSubCategory.lstProducts = new List<clsProducts>();

                    lstProductSubCategories.Add(clsProductSubCategory);
                }
            }

            return lstProductSubCategories;
        }

        //Get
        public clsProductSubCategories getProductSubCategoryByID(int iProductSubCategoryID)
        {
            clsProductSubCategories clsProductSubCategory = null;
            tblProductSubCategories tblProductSubCategories = db.tblProductSubCategories.FirstOrDefault(ProductSubCategory => ProductSubCategory.iProductSubCategoryID == iProductSubCategoryID && ProductSubCategory.bIsDeleted == false);

            if (tblProductSubCategories != null)
            {
                //Manager
                clsProductsManager clsProductsManager = new clsProductsManager();

                clsProductSubCategory = new clsProductSubCategories();

                clsProductSubCategory.iProductSubCategoryID = tblProductSubCategories.iProductSubCategoryID;
                clsProductSubCategory.dtAdded = tblProductSubCategories.dtAdded;
                clsProductSubCategory.iAddedBy = tblProductSubCategories.iAddedBy;
                clsProductSubCategory.dtEdited = tblProductSubCategories.dtEdited;
                clsProductSubCategory.iEditedBy = tblProductSubCategories.iEditedBy;

                clsProductSubCategory.strTitle = tblProductSubCategories.strTitle;
                clsProductSubCategory.bIsDeleted = tblProductSubCategories.bIsDeleted;

                clsProductSubCategory.lstProducts = new List<clsProducts>();

                if (tblProductSubCategories.tblProducts.Count > 0)
                {
                    foreach (var ProductItem in tblProductSubCategories.tblProducts)
                    {
                        clsProducts clsProduct = clsProductsManager.convertProductsTableToClass(ProductItem);
                        clsProductSubCategory.lstProducts.Add(clsProduct);
                    }
                }
            }

            return clsProductSubCategory;
        }

        //Save
        public void saveProductSubCategory(clsProductSubCategories clsProductSubCategory)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblProductSubCategories tblProductSubCategories = new tblProductSubCategories();

                tblProductSubCategories.iProductSubCategoryID = clsProductSubCategory.iProductSubCategoryID;

                tblProductSubCategories.strTitle = clsProductSubCategory.strTitle;
                tblProductSubCategories.bIsDeleted = clsProductSubCategory.bIsDeleted;

                //Add
                if (tblProductSubCategories.iProductSubCategoryID == 0)
                {
                    tblProductSubCategories.dtAdded = DateTime.Now;
                    tblProductSubCategories.iAddedBy = clsCMSUser.iCMSUserID;
                    tblProductSubCategories.dtEdited = DateTime.Now;
                    tblProductSubCategories.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblProductSubCategories.Add(tblProductSubCategories);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblProductSubCategories.dtAdded = clsProductSubCategory.dtAdded;
                    tblProductSubCategories.iAddedBy = clsProductSubCategory.iAddedBy;
                    tblProductSubCategories.dtEdited = DateTime.Now;
                    tblProductSubCategories.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblProductSubCategories>().AddOrUpdate(tblProductSubCategories);
                    db.SaveChanges();
                }
            }
        }

        //Save
        public int saveProductSubCategoryAndGetID(clsProductSubCategories clsProductSubCategory)
        {
            int iProductSubCategoryID = 0;

            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblProductSubCategories tblProductSubCategories = new tblProductSubCategories();

                tblProductSubCategories.iProductSubCategoryID = clsProductSubCategory.iProductSubCategoryID;

                tblProductSubCategories.strTitle = clsProductSubCategory.strTitle;
                tblProductSubCategories.bIsDeleted = clsProductSubCategory.bIsDeleted;

                //Add
                if (tblProductSubCategories.iProductSubCategoryID == 0)
                {
                    tblProductSubCategories.dtAdded = DateTime.Now;
                    tblProductSubCategories.iAddedBy = clsCMSUser.iCMSUserID;
                    tblProductSubCategories.dtEdited = DateTime.Now;
                    tblProductSubCategories.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblProductSubCategories.Add(tblProductSubCategories);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblProductSubCategories.dtAdded = clsProductSubCategory.dtAdded;
                    tblProductSubCategories.iAddedBy = clsProductSubCategory.iAddedBy;
                    tblProductSubCategories.dtEdited = DateTime.Now;
                    tblProductSubCategories.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblProductSubCategories>().AddOrUpdate(tblProductSubCategories);
                    db.SaveChanges();
                }

                iProductSubCategoryID = tblProductSubCategories.iProductSubCategoryID;
            }

            return iProductSubCategoryID;
        }

        //Remove
        public void removeProductSubCategoryByID(int iProductSubCategoryID)
        {
            tblProductSubCategories tblProductSubCategory = db.tblProductSubCategories.Find(iProductSubCategoryID);
            if (tblProductSubCategory != null)
            {
                tblProductSubCategory.bIsDeleted = true;
                db.Entry(tblProductSubCategory).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check
        public bool checkIfProductSubCategoryExists(int iProductSubCategoryID)
        {
            bool bProductSubCategoryExists = db.tblProductSubCategories.Any(ProductSubCategory => ProductSubCategory.iProductSubCategoryID == iProductSubCategoryID && ProductSubCategory.bIsDeleted == false);
            return bProductSubCategoryExists;
        }

        //Convert database table to class
        public clsProductSubCategories convertProductSubCategoriesTableToClass(tblProductSubCategories tblProductSubCategory)
        {
            clsProductSubCategories clsProductSubCategory = new clsProductSubCategories();

            clsProductSubCategory.iProductSubCategoryID = tblProductSubCategory.iProductSubCategoryID;
            clsProductSubCategory.dtAdded = tblProductSubCategory.dtAdded;
            clsProductSubCategory.iAddedBy = tblProductSubCategory.iAddedBy;
            clsProductSubCategory.dtEdited = tblProductSubCategory.dtEdited;
            clsProductSubCategory.iEditedBy = tblProductSubCategory.iEditedBy;

            clsProductSubCategory.strTitle = tblProductSubCategory.strTitle;
            clsProductSubCategory.bIsDeleted = tblProductSubCategory.bIsDeleted;

            return clsProductSubCategory;
        }
    }
}
