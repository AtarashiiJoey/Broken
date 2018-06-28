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
    public class clsProductSizesManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsProductSizes> getAllProductSizesList()
        {
            List<clsProductSizes> lstProductSizes = new List<clsProductSizes>();
            var lstGetProductSizesList = db.tblProductSizes.Where(ProductSize => ProductSize.bIsDeleted == false).ToList();

            if (lstGetProductSizesList.Count > 0)
            {
                //Manager
                clsProductsManager clsProductsManager = new clsProductsManager();

                foreach (var item in lstGetProductSizesList)
                {
                    clsProductSizes clsProductSize = new clsProductSizes();

                    clsProductSize.iProductSizeID = item.iProductSizeID;
                    clsProductSize.dtAdded = item.dtAdded;
                    clsProductSize.iAddedBy = item.iAddedBy;
                    clsProductSize.dtEdited = item.dtEdited;
                    clsProductSize.iEditedBy = item.iEditedBy;

                    clsProductSize.strSize = item.strSize;
                    clsProductSize.dblPrice = item.dblPrice;
                    clsProductSize.iQuantityAvailable = item.iQuantityAvailable;
                    clsProductSize.iProductID = item.iProductID;
                    clsProductSize.bIsDeleted = item.bIsDeleted;

                    if (item.tblProducts != null)
                        clsProductSize.clsProduct = clsProductsManager.convertProductsTableToClass(item.tblProducts);

                    lstProductSizes.Add(clsProductSize);
                }
            }

            return lstProductSizes;
        }

        //Get All
        public List<clsProductSizes> getAllProductSizesOnlyList()
        {
            List<clsProductSizes> lstProductSizes = new List<clsProductSizes>();
            var lstGetProductSizesList = db.tblProductSizes.Where(ProductSize => ProductSize.bIsDeleted == false).ToList();

            if (lstGetProductSizesList.Count > 0)
            {
                foreach (var item in lstGetProductSizesList)
                {
                    clsProductSizes clsProductSize = new clsProductSizes();

                    clsProductSize.iProductSizeID = item.iProductSizeID;
                    clsProductSize.dtAdded = item.dtAdded;
                    clsProductSize.iAddedBy = item.iAddedBy;
                    clsProductSize.dtEdited = item.dtEdited;
                    clsProductSize.iEditedBy = item.iEditedBy;

                    clsProductSize.strSize = item.strSize;
                    clsProductSize.dblPrice = item.dblPrice;
                    clsProductSize.iQuantityAvailable = item.iQuantityAvailable;
                    clsProductSize.iProductID = item.iProductID;
                    clsProductSize.bIsDeleted = item.bIsDeleted;

                    lstProductSizes.Add(clsProductSize);
                }
            }

            return lstProductSizes;
        }

        //Get
        public clsProductSizes getProductSizeByID(int iProductSizeID)
        {
            clsProductSizes clsProductSize = null;
            tblProductSizes tblProductSizes = db.tblProductSizes.FirstOrDefault(ProductSize => ProductSize.iProductSizeID == iProductSizeID && ProductSize.bIsDeleted == false);

            if (tblProductSizes != null)
            {
                //Manager
                clsProductsManager clsProductsManager = new clsProductsManager();

                clsProductSize = new clsProductSizes();

                clsProductSize.iProductSizeID = tblProductSizes.iProductSizeID;
                clsProductSize.dtAdded = tblProductSizes.dtAdded;
                clsProductSize.iAddedBy = tblProductSizes.iAddedBy;
                clsProductSize.dtEdited = tblProductSizes.dtEdited;
                clsProductSize.iEditedBy = tblProductSizes.iEditedBy;

                clsProductSize.strSize = tblProductSizes.strSize;
                clsProductSize.dblPrice = tblProductSizes.dblPrice;
                clsProductSize.iQuantityAvailable = tblProductSizes.iQuantityAvailable;
                clsProductSize.iProductID = tblProductSizes.iProductID;
                clsProductSize.bIsDeleted = tblProductSizes.bIsDeleted;

                if (tblProductSizes.tblProducts != null)
                    clsProductSize.clsProduct = clsProductsManager.convertProductsTableToClass(tblProductSizes.tblProducts);
            }

            return clsProductSize;
        }

        //Save
        public void saveProductSize(clsProductSizes clsProductSize)
        {
            if (HttpContext.Current.Session["clsCMSUser"] != null)
            {
                clsCMSUsers clsCMSUser = (clsCMSUsers)HttpContext.Current.Session["clsCMSUser"];
                tblProductSizes tblProductSizes = new tblProductSizes();

                tblProductSizes.iProductSizeID = clsProductSize.iProductSizeID;

                tblProductSizes.strSize = clsProductSize.strSize;
                tblProductSizes.dblPrice = clsProductSize.dblPrice;
                tblProductSizes.iQuantityAvailable = clsProductSize.iQuantityAvailable;
                tblProductSizes.iProductID = clsProductSize.iProductID;
                tblProductSizes.bIsDeleted = clsProductSize.bIsDeleted;

                //Add
                if (tblProductSizes.iProductSizeID == 0)
                {
                    tblProductSizes.dtAdded = DateTime.Now;
                    tblProductSizes.iAddedBy = clsCMSUser.iCMSUserID;
                    tblProductSizes.dtEdited = DateTime.Now;
                    tblProductSizes.iEditedBy = clsCMSUser.iCMSUserID;

                    db.tblProductSizes.Add(tblProductSizes);
                    db.SaveChanges();
                }
                //Update
                else
                {
                    tblProductSizes.dtAdded = clsProductSize.dtAdded;
                    tblProductSizes.iAddedBy = clsProductSize.iAddedBy;
                    tblProductSizes.dtEdited = DateTime.Now;
                    tblProductSizes.iEditedBy = clsCMSUser.iCMSUserID;

                    db.Set<tblProductSizes>().AddOrUpdate(tblProductSizes);
                    db.SaveChanges();
                }
            }
        }

        //Remove
        public void removeProductSizeByID(int iProductSizeID)
        {
            tblProductSizes tblProductSize = db.tblProductSizes.Find(iProductSizeID);
            if (tblProductSize != null)
            {
                tblProductSize.bIsDeleted = true;
                db.Entry(tblProductSize).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Check
        public bool checkIfProductSizeExists(int iProductSizeID)
        {
            bool bProductSizeExists = db.tblProductSizes.Any(ProductSize => ProductSize.iProductSizeID == iProductSizeID && ProductSize.bIsDeleted == false);
            return bProductSizeExists;
        }

        //Convert database table to class
        public clsProductSizes convertProductSizesTableToClass(tblProductSizes tblProductSize)
        {
            clsProductSizes clsProductSize = new clsProductSizes();

            clsProductSize.iProductSizeID = tblProductSize.iProductSizeID;
            clsProductSize.dtAdded = tblProductSize.dtAdded;
            clsProductSize.iAddedBy = tblProductSize.iAddedBy;
            clsProductSize.dtEdited = tblProductSize.dtEdited;
            clsProductSize.iEditedBy = tblProductSize.iEditedBy;

            clsProductSize.strSize = tblProductSize.strSize;
            clsProductSize.dblPrice = tblProductSize.dblPrice;
            clsProductSize.iQuantityAvailable = tblProductSize.iQuantityAvailable;
            clsProductSize.iProductID = tblProductSize.iProductID;
            clsProductSize.bIsDeleted = tblProductSize.bIsDeleted;

            return clsProductSize;
        }
    }
}
