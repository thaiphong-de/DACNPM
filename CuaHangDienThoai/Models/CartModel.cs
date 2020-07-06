using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuaHangMayTinh.Models
{
    public class CartModel
    {
        public static List<ProductCartDTO> List = new List<ProductCartDTO>();

        public void AddToCart(ProductCartDTO productCartDTO)
        {
            AddToCart(productCartDTO.Product, productCartDTO.Quantity);
        }
        public void AddToCart(SanPham product, int quantity)
        {
            bool flag = false;
            foreach (ProductCartDTO item in List)
            {
                if (item.Product == null) return;
                if (item.Product.ma.Trim().Equals(product.ma.Trim()))
                {
                    flag = true;
                    item.Quantity += quantity;
                    break;
                }
            }
            if (!flag)
            {
                List.Add(new ProductCartDTO(product, quantity));
            }
        }
    }
}