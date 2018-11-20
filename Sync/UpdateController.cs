using Colmart;
using Colmart.Model_Manager;
using Colmart.Sync.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MsgApp.Controllers
{
    public class UpdateController
    {
        ColmartDBContext db = new ColmartDBContext();
        ColmartViewsDBContext dbV = new ColmartViewsDBContext();

        public void UpdateProductsFromColmart()
        {
            // Managers
            var clsPSM = new clsProductSizesManager();
            var x = 1;
            //var lstColmartProduct = PopulateClsColmartProducts();

            //Making the lists so that they can be shortened easily


            // checking db items 1 by 1
            var Colmart = from colmart in dbV.ColmartProducts select colmart;
            var Products = from product in db.tblProducts select product;
            foreach (var product in Products)
            {
                // get product details if not deleted
                if (!product.bIsDeleted)
                {
                    var ProductID = product.iProductID;
                    var StyleCode = product.strStyleCode;
                    var Title = product.strTitle;
                    var ImageURL = product.strImageURL;

                    var lstSoftserveProductSize = clsPSM.getAllProductSizesListByID(ProductID);

                    foreach (var productSizes in lstSoftserveProductSize)
                    {
                        // get product size details if not deleted AND the ID's match
                        if ((!productSizes.bIsDeleted) && (productSizes.iProductID == ProductID))
                        {
                            var Size = productSizes.strSize;
                            var QuantityAvailable = productSizes.iQuantityAvailable;
                            var Price = productSizes.dblPrice;
                            foreach (var colmart in Colmart)
                            {
                                // use style code + size to add up all the qty-onhands for 3 branches
                                if (colmart.StyleCode == StyleCode && colmart.Size == Size && colmart.Title == Title)
                                {
                                    Debug.WriteLine($"{x++} viable products found");
                                }
                            }

                        }
                        else
                        {
                            Debug.WriteLine("This Product exists but the productSizes Details are removed");
                        }
                    }
                }
                else
                {
                    Debug.WriteLine($"product with ID:{product.iProductID} has been soft deleted");
                }

            }


            // compare old and new

        }

        public void UpdateUsersFromColmart()
        {

        }

        public List<clsColmartProducts> PopulateClsColmartProducts()
        {
            var lst = new List<clsColmartProducts>();
            return lst;
        }
    }

}