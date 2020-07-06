namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietHoaDonBans = new HashSet<ChiTietHoaDonBan>();
        }

        [Key]
        [StringLength(5)]
        [Display(Name = "Mã")]
        public string ma { get; set; }

        [StringLength(255)]
        [Display(Name = "Tên")]
        public string ten { get; set; }

        [Display(Name = "Giá cũ")]
        public double gia_cu { get; set; }

        [Display(Name = "Giá mới")]
        public double gia_moi { get; set; }

        [StringLength(255)]
        [Display(Name = "Hình ảnh")]
        public string hinh_anh { get; set; }

        [Display(Name = "Hot")]
        public bool? hot { get; set; }

        [Display(Name = "Mô tả")]
        public string mo_ta { get; set; }

        [Display(Name = "Mã danh mục")]
        public int? ma_danh_muc { get; set; }

        [StringLength(5)]
        [Display(Name = "Mã nhà cung cấp")]
        public string ma_nha_cung_cap { get; set; }

        [Display(Name = "Số lượt xem")]
        public long? so_luot_xem { get; set; }

        [Display(Name = "Số lượt mua")]
        public long? so_luot_mua { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? ngay_tao { get; set; }

        [Display(Name = "Số lượng")]
        public int? so_luong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonBan> ChiTietHoaDonBans { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
