using Colmart.Model_Manager;
using Colmart.Models;
using Colmart.Sync.Model_Manager;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Colmart.Sync
{
    public class UpdateController
    {
        ColmartDBContext db = new ColmartDBContext();
        clsProductsManager clsProductsManager = new clsProductsManager();
        clsProductSizesManager clsProductSizesManager = new clsProductSizesManager();


        readonly string logPath = @"C:\Users\Dev-2018-Oct-PC\Desktop\Colmart\RepoVPS\Colmart\Colmart\Sync\UpdateLog.txt";

        public void UcThreader(int threadCalled)
        {
            Thread thread;
            switch (threadCalled)
            {
                case 1:
                    thread = new Thread(UpdateProductsFromColmart);
                    thread.Start();
                    Debug.WriteLine("Product thread started");
                    break;
                case 2:
                    {
                        thread = new Thread(UpdateUsersFromColmart);
                        thread.Start();
                        Debug.WriteLine("User thread started");
                        break;
                    }
            }
        }

        public void UpdateProductsFromColmart()
        {
            #region Logging timer
            var startTime = DateTime.Now;

            if (!File.Exists(logPath))
            {
                File.Create(logPath).Dispose();

                using (TextWriter tw = new StreamWriter(logPath))
                {
                    tw.WriteLine("Colmart Polling log:");
                }
            }
            else if (File.Exists(logPath))
            {
                using (var tw = new StreamWriter(logPath, true))
                {
                    tw.WriteLine($"{DateTime.Now} : UPDATE STOCK FROM COLMART");
                }
            }
            #endregion
            // Managers
            var clsPsm = new clsProductSizesManager();
            var clsCpm = new clsColmartProductManager();

            // Database Call

            var productsSsd = db.tblProducts.Where(products => products.bIsDeleted != true).ToList();

            foreach (var product in productsSsd)
            {
                // Shortlists from Managers
                var lstSoftserveProductSize = clsPsm.getAllProductSizesListByID(product.iProductID);
                var lstColmartProduct = clsCpm.getAllColmartProductListByStyle(product.strStyleCode);

                using (var tw = new StreamWriter(logPath, true))
                {
                    tw.WriteLine($"Product id: {product.iProductID,-15} HAS : ================================");
                }
                foreach (var productSizes in lstSoftserveProductSize)
                {
                    var stockDbn = 0;
                    var stockJhb = 0;
                    var stockCpt = 0;
                    var stockTotalcounter = 0;
                    float stockCost = 0;

                    if (productSizes.iProductID == product.iProductID)
                    {
                        stockCost = (float)productSizes.dblPrice;
                        foreach (var colmart in lstColmartProduct)
                        {
                            if ((colmart.StyleCode == product.strStyleCode) && (colmart.Size == productSizes.strSize))
                            {
                                switch (colmart.Branch.Replace(" ", ""))
                                {
                                    case "DBN":
                                        stockDbn = (int)colmart.Qty_OnHand;
                                        stockTotalcounter += stockDbn;
                                        break;
                                    case "JHB":
                                        stockJhb = (int)colmart.Qty_OnHand;
                                        stockTotalcounter += stockJhb;
                                        break;
                                    case "CPT":
                                        stockCpt = (int)colmart.Qty_OnHand;
                                        stockTotalcounter += stockCpt;
                                        break;
                                }
                            }

                            // Here be DB dragons
                            if ((product.strStyleCode != colmart.StyleCode) ||
                                (product.strImageURL != colmart.ImageURL) ||
                                (product.strTitle != colmart.Title))
                            {
                                var clsProducts = new clsProducts
                                {
                                    iProductID = product.iProductID,
                                    iAddedBy = product.iAddedBy,
                                    dtAdded = product.dtAdded,
                                    iEditedBy = product.iEditedBy,
                                    dtEdited = DateTime.Now,
                                    strProductColour = product.strProductColour,
                                    strFullDescription = product.strFullDescription,
                                    iProductSubCategoryID = product.iProductSubCategoryID,
                                    iProductCategoryID = product.iProductCategoryID,
                                    bIsDeleted = product.bIsDeleted,
                                    strStyleCode = colmart.StyleCode,
                                    strImageURL = colmart.ImageURL,
                                    strTitle = colmart.Title
                                };
                                //Save product
                                clsProductsManager.saveProduct(clsProducts);
                                Debug.WriteLine($"Changed {product.iProductID}");
                            }
                           
                            
                            if (((Math.Round((double)productSizes.dblPrice,2)) != (Math.Round((double)stockCost,2))) ||
                                (productSizes.iQuantityAvailable != stockTotalcounter))
                            {
                                var clsProductSizes = new clsProductSizes()
                                {
                                    iProductSizeID = productSizes.iProductSizeID,
                                    iAddedBy = productSizes.iAddedBy,
                                    dtAdded = productSizes.dtAdded,
                                    iEditedBy = productSizes.iEditedBy,
                                    dtEdited = DateTime.Now,
                                    dblPrice = (Math.Round((double)stockCost)),
                                    iProductID = productSizes.iProductID,
                                    iQuantityAvailable = stockTotalcounter,
                                    strSize = productSizes.strSize,
                                    bIsDeleted = productSizes.bIsDeleted
                                };
                                //Save productSizes
                                clsProductSizesManager.saveProductSize(clsProductSizes);
                                Debug.WriteLine($"Changed ProductSizes: {productSizes.iProductSizeID}");
                            }
                            
                            // Dragons slain
                        }

                    }
                    using (var tw = new StreamWriter(logPath, true))
                    {
                        tw.WriteLine($"Size[{productSizes.strSize,5}] DBN[{stockDbn,3}] JHB[{stockJhb,3}] CPT[{stockCpt,3}]  Tot[{stockTotalcounter,3}] R[{stockCost,6}]");
                    }
                }
                using (var tw = new StreamWriter(logPath, true))
                {
                    tw.WriteLine($"Product id: {product.iProductID,-15} END : ================================\r\n");
                }
            }
            using (var tw = new StreamWriter(logPath, true))
            {
                tw.WriteLine($"{DateTime.Now} : Total up-time {(int)Math.Floor((DateTime.Now - startTime).TotalSeconds)} seconds\r\n\r\n");
            }
            Debug.WriteLine($"{DateTime.Now} : Total up-time {(int)Math.Floor((DateTime.Now - startTime).TotalSeconds)} seconds\r\n\r\n");
        }

        public void UpdateUsersFromColmart()
        {

        }

    }

}