using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colmart.Sync.Models;

namespace Colmart.Sync.Model_Manager
{
    public class clsColmartProductManager
    {
        ColmartViewsDBContext dbv = new ColmartViewsDBContext();

        //Get All
        public List<clsColmartProducts> getAllColmartProductList()
        {
            List<clsColmartProducts> lstProductSizes = new List<clsColmartProducts>();
            var lstGetColmartProductList = dbv.ColmartProducts.ToList();

            if (lstGetColmartProductList.Count > 0)
            {
                foreach (var item in lstGetColmartProductList)
                {
                    clsColmartProducts clsColmartProducts = new clsColmartProducts
                    {
                    };
                    lstProductSizes.Add(clsColmartProducts);
                }
            }

            return lstProductSizes;
        }

        //Get All
        public List<clsColmartProducts> getAllColmartProductOnlyList()
        {
            List<clsColmartProducts> lstProductSizes = new List<clsColmartProducts>();
            var lstGetColmartProductList = dbv.ColmartProducts.ToList();

            if (lstGetColmartProductList.Count > 0)
            {
                foreach (var item in lstGetColmartProductList)
                {
                    clsColmartProducts clsColmartProducts = new clsColmartProducts();

                    

                    lstProductSizes.Add(clsColmartProducts);
                }
            }

            return lstProductSizes;
        }

        //Get By ID
        public List<clsColmartProducts> getAllColmartProductListByID(string StyleCode, string Title)
        {
            List<clsColmartProducts> lstProductSizes = new List<clsColmartProducts>();
            var lstGetColmartProductList = dbv.ColmartProducts.Where(Product => Product.StyleCode == StyleCode && Product.Title == Title).ToList();

            if (lstGetColmartProductList.Count > 0)
            {
                foreach (var item in lstGetColmartProductList)
                {
                    clsColmartProducts clsColmartProducts = new clsColmartProducts();
                    
                    lstProductSizes.Add(clsColmartProducts);
                }
            }

            return lstProductSizes;
        }
    }
}