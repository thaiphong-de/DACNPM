using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuaHangMayTinh.Models
{
    public class ProductCartDTO
    {
        public SanPham Product { get; set; }
        public int Quantity { get; set; }
        public ProductCartDTO() {  }
        public ProductCartDTO(SanPham product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}