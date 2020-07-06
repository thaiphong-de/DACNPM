using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuaHangMayTinh.Areas.Admin.Models
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            
        }
        public HttpPostedFileBase file { get; set; }
        public SanPham sanPham { get; set; }

    }
}