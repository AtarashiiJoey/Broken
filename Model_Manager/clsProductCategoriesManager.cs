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
    public class clsProductCategoriesManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsProductCategories> getAllProductCategoriesList()
        {
            List<clsProductCategories> lstProductCategories = new List<clsProductCategories>();
            var lstGetProductCategoriesList = db.tblProductCategories.Where(ProductCategory => ProductCategory.bIsDeleted == false).ToList();

            if (lstGetProductCategoriesList.Count > 0)
            {
                //Manager
                clsProductsManager clsProductsManager = new clsProductsManager();

                foreach (var item in lstGetProductCategoriesList)
                {
                    clsProductCategories clsProductCategory = new clsProductCategories();

                    clsProductCategory.iProductCategoryID = item.iProductCategoryID;
                    clsProductCategory.dtAdded = item.dtAdded;
                    clsProductCategory.iAddedBy = item.iAddedBy;
                    clsProductCategory.dtEdited = item.dtEdited;
                    clsProductCategory.iEditedBy = item.iEditedBy;

                    clsProductCategory.strTitle = item.strTitle;
                    clsProductCategory.bIsDeleted = item.bIsDeleted;

                    clsProductCategory.lstProducts = new List<clsProducts>();

                    if (item.tblProducts.Count > 0)
                    {
                        foreach (var ProductItem in item.tblProducts)
                        {
                            clsProducts clsProduct = clsProductsManager.convertProductsTableToClass(ProductItem);
                            clsProductCategory.lstProducts.Add(clsProduct);
                        }
                    }

                    lstProductCategories.Add(clsProductCategory);
                }
            }

            return lstProductCategories;
        }

        //Get All
        public List<clsProductCategories> getAllProductCategoriesOnlyList()
        {
            List<clsProductCategories> lstProductCategories = new List<clsProductCategories>();
            var lstGetProductCategoriesList = db.tblProductCategories.Where(ProductCategory => ProductCategory.bIsDeleted == false).ToList();

            if (lstGetProductCategoriesList.Count > 0)
            {
                //Manager
                clsProductsManager clsProductsManager = new clsProductsManager();

                foreach (var item in lstGetProductCategoriesList)
                {
                    clsProductCategories clsProductCategory = new clsProductCategories();

                    clsProductCategory.iProductCategoryID = item.iProductCategoryID;
                    clsProductCategory.dtAdded = item.dtAdded;
                    clsProductCategory.iAddedBy = item.iAddedBy;
                    clsProductCategory.dtEdited = item.dtEdited;
                    clsProductCategory.iEditedBy = item.iEditedBy;

                    clsProductCategory.strTitle = item.strTitle;
                    clsProductCategory.bIsDeleted = item.bIsDeleted;

                    clsProductCategory.lstProducts = new List<clsProducts>();

                    lstProductCategories.Add(clsProductCategory);
                }
            }

            return lstProductCategories;
        }

        //Get
        public clsProductCategories getProductCategoryByID(int iProductCategoryID)
        {
            clsProductCategories clsProductCategory = null;
            tblProductCategories tblProductCategories = db.tblProductCategories.FirstOrDefault(ProductCategory => ProductCategory.iProductCategoryID == iProductCategoryID && ProductCategory.bIsDeleted == false);

            if (tblProductCategories != null)
            {
                //Manager
                clsProductsManager clsProductsManager = new clsProductsManager();

                clsProductCategory = new clsProductCategories();

                clsProductCategory.iProductCategoryID = tblProductCategories.iProductCategoryID;
                clsProductCategory.dtAdded = tblProductCategories.dtAdded;
                clsProductCategory.iAddedBy = tblProductCategories.iAddedBy;
                clsProductCategory.dtEdited = tblProductCategories.dtEdited;
                clsProductCategory.iEditedBy = tblProductCategories.iEditedBy;

                clsProductCategory.strTitle = tblProductCategories.strTitle;
                clsProductCategory.bIsDeleted = tblProductCategories.bIsDeleted;

                clsProductCategory.lstProducts = new List<clsProducts>();

                if (tblProductCategories.tblProducts.Count > 0)
                {
                    foreach (var ProductItem in tblProductCategories.tblProducts)
                    {
                        clsProducts clsProduct = clsProductsManager.convertProductsTableToClass(ProductItem);
                        clsProductCategory.lstProducts.Add(clsProduct);
                    }
                }
            }

            return clsProductCategory;
        }

        //Save
        public void saveProductCategory(clsProductCategories clsProductCategory)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblProductCategories tblProductCategories = new tblProductCategories();

                tblProductCategories.iProductCategoryID = clsProductCategory.iProductCategoryID;

                tblProductCategories.strTitle = clsProductCategory.strTitle;
                tblProductCategories.bIsDeleted = clsProductCategory.bIsDeleted;

                //Add
                if (tblProductCategories.iProductCategoryID == 0)
                {
                    tblProductCategories.dtAdded = DateTime.Now;
                    tblProductCategories.iAddedBy = clsCMSUser.iCMSUserID;
                    tblProductCategories.dtEdited = DateTime.Now;
                    tblProductCategories.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblProductCategories.Add(tblProductCategories);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblProductCategories.dtAdded = clsProductCategory.dtAdded;
                    tblProductCategories.iAddedBy = clsProductCategory.iAddedBy;
                    tblProductCategories.dtEdited = DateTime.Now;
                    tblProductCategories.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblProductCategories>().AddOrUpdate(tblProductCategories);
                    db.SaveChanges();
                }
            }
        }

        //Save
        public int saveProductCategoryAndGetID(clsProductCategories clsProductCategory)
        {
            int iProductCategoryID = 0;

            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblProductCategories tblProductCategories = new tblProductCategories();

                tblProductCategories.iProductCategoryID = clsProductCategory.iProductCategoryID;

                tblProductCategories.strTitle = clsProductCategory.strTitle;
                tblProductCategories.bIsDeleted = clsProductCategory.bIsDeleted;

                //Add
                if (tblProductCategories.iProductCategoryID == 0)
                {
                    tblProductCategories.dtAdded = DateTime.Now;
                    tblProductCategories.iAddedBy = clsCMSUser.iCMSUserID;
                    tblProductCategories.dtEdited = DateTime.Now;
                    tblProductCategories.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblProductCategories.Add(tblProductCategories);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblProductCategories.dtAdded = clsProductCategory.dtAdded;
                    tblProductCategories.iAddedBy = clsProductCategory.iAddedBy;
                    tblProductCategories.dtEdited = DateTime.Now;
                    tblProductCategories.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblProductCategories>().AddOrUpdate(tblProductCategories);
                    db.SaveChanges();
                }

                iProductCategoryID = tblProductCategories.iProductCategoryID;
            }

            return iProductCategoryID;
        }

        //Remove
        public void removeProductCategoryByID(int iProductCategoryID)
        {
            tblProductCategories tblProductCategory = db.tblProductCategories.Find(iProductCategoryID);
            if (tblProductCategory != null)
            {
                tblProductCategory.bIsDeleted = true;
                db.Entry(tblProductCategory).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check
        public bool checkIfProductCategoryExists(int iProductCategoryID)
        {
            bool bProductCategoryExists = db.tblProductCategories.Any(ProductCategory => ProductCategory.iProductCategoryID == iProductCategoryID && ProductCategory.bIsDeleted == false);
            return bProductCategoryExists;
        }

        //Convert database table to class
        public clsProductCategories convertProductCategoriesTableToClass(tblProductCategories tblProductCategory)
        {
            clsProductCategories clsProductCategory = new clsProductCategories();

            clsProductCategory.iProductCategoryID = tblProductCategory.iProductCategoryID;
            clsProductCategory.dtAdded = tblProductCategory.dtAdded;
            clsProductCategory.iAddedBy = tblProductCategory.iAddedBy;
            clsProductCategory.dtEdited = tblProductCategory.dtEdited;
            clsProductCategory.iEditedBy = tblProductCategory.iEditedBy;

            clsProductCategory.strTitle = tblProductCategory.strTitle;
            clsProductCategory.bIsDeleted = tblProductCategory.bIsDeleted;

            return clsProductCategory;
        }
    }
}
