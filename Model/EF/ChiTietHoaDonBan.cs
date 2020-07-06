namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDonBan")]
    public partial class ChiTietHoaDonBan
    {
        [Key]
        [Display(Name = "Mã")]
        public int ma { get; set; }

        [StringLength(5)]
        [Display(Name = "Mã sản phẩm")]
        public string ma_san_pham { get; set; }

        [Display(Name = "Đơn giá")]
        public double? don_gia { get; set; }

        [Display(Name = "Số lượng")]
        public int? so_luong { get; set; }

        [Display(Name = "Mã hóa đơn")]
        public int? ma_hoa_don { get; set; }

        public virtual HoaDonBan HoaDonBan { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
