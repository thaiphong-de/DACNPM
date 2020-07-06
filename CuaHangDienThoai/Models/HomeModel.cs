using Model.EF;
using System.Collections.Generic;

namespace CuaHangMayTinh.Models
{
    public class HomeModel
    {
        public List<SanPham> LastestProducts { get; set; }
        public List<SanPham> HotProducts { get; set; }
        public List<SanPham> TopSellerProducts { get; set; }
        public List<ProductCartDTO> RecentlyViewedProducts { get; set; }
        public List<SanPham> TopViewedProducts { get; set; }
}
}