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

        #region Get All : getAllColmartProductList
        public List<clsColmartProducts> getAllColmartProductList()
        {
            var lstProductSizes = new List<clsColmartProducts>();
            var lstGetColmartProductList = dbv.ColmartProducts.ToList();

            if (lstGetColmartProductList.Count > 0)
            {
                foreach (var item in lstGetColmartProductList)
                {
                    var clsColmartProducts = new clsColmartProducts
                    {
                        fProductStockID = item.fProductStockID,
                        Branch = item.Branch,
                        Category = item.Category,
                        ImageURL = item.ImageURL,
                        Price = item.Price,
                        Qty_OnHand = item.Qty_OnHand,
                        Size = item.Size,
                        SizeOrdinal = item.SizeOrdinal,
                        StyleCode = item.StyleCode,
                        Title = item.Title
                    };

                    lstProductSizes.Add(clsColmartProducts);
                }

                return lstProductSizes;
            }
            return lstProductSizes;
        }
        #endregion

        #region Get Style and Title
        public List<clsColmartProducts> getAllColmartProductListByStyle(string StyleCode)
        {
            List<clsColmartProducts> lstProductSizes = new List<clsColmartProducts>();
            var lstGetColmartProductList = dbv.ColmartProducts.Where(product => product.StyleCode == StyleCode).ToList();

            if (lstGetColmartProductList.Count > 0)
            {
                foreach (var item in lstGetColmartProductList)
                {
                    clsColmartProducts clsColmartProducts = new clsColmartProducts()
                    {
                        fProductStockID = item.fProductStockID,
                        Branch = item.Branch,
                        Category = item.Category,
                        ImageURL = item.ImageURL,
                        Price = item.Price,
                        Qty_OnHand = item.Qty_OnHand,
                        Size = item.Size,
                        SizeOrdinal = item.SizeOrdinal,
                        StyleCode = item.StyleCode,
                        Title = item.Title
                    };
                    lstProductSizes.Add(clsColmartProducts);
                }
            }

            return lstProductSizes;
        }
        #endregion
    }
}