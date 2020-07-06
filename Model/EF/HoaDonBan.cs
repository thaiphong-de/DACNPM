namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonBan")]
    public partial class HoaDonBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDonBan()
        {
            ChiTietHoaDonBans = new HashSet<ChiTietHoaDonBan>();
        }

        [Key]
        [Display(Name = "Mã")]
        public int ma { get; set; }

        [StringLength(255)]
        [Display(Name = "Mã khách hàng")]
        public string khach_hang { get; set; }

        [StringLength(255)]
        [Display(Name = "Tên khách hàng")]
        public string ten_khach_hang { get; set; }

        [StringLength(255)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [StringLength(255)]
        [Display(Name = "Địa chỉ")]
        public string dia_chi { get; set; }

        [StringLength(20)]
        [Display(Name = "Số điện thoại")]
        public string so_dien_thoai { get; set; }

        [StringLength(255)]
        [Display(Name = "Tình trạng")]
        public string tinh_trang { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? ngay_tao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonBan> ChiTietHoaDonBans { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
