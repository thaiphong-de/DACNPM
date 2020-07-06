namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MobileStore : DbContext
    {
        public MobileStore()
            : base("name=MOBILESTORE2")
        {
        }

        public virtual DbSet<ChiTietHoaDonBan> ChiTietHoaDonBans { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<HoaDonBan> HoaDonBans { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<Tin> Tins { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHoaDonBan>()
                .Property(e => e.ma_san_pham)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.DanhMuc)
                .HasForeignKey(e => e.ma_danh_muc);

            modelBuilder.Entity<HoaDonBan>()
                .Property(e => e.khach_hang)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonBan>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonBan>()
                .Property(e => e.so_dien_thoai)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonBan>()
                .Property(e => e.tinh_trang)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonBan>()
                .HasMany(e => e.ChiTietHoaDonBans)
                .WithOptional(e => e.HoaDonBan)
                .HasForeignKey(e => e.ma_hoa_don);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.ma)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.NhaCungCap)
                .HasForeignKey(e => e.ma_nha_cung_cap);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.ma)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinh_anh)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.mo_ta)
                .IsUnicode(true);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.ma_nha_cung_cap)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietHoaDonBans)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.ma_san_pham);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.tai_khoan)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.mat_khau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.hinh_anh)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.so_dien_thoai)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.HoaDonBans)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.khach_hang);

            modelBuilder.Entity<BinhLuan>()
                .Property(e => e.tai_khoan)
                .IsUnicode(false);

            modelBuilder.Entity<BinhLuan>()
                .Property(e => e.ma_san_pham)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tin>()
                .Property(e => e.ma)
                .IsFixedLength();

            modelBuilder.Entity<Tin>()
                .Property(e => e.tai_khoan)
                .IsUnicode(false);
        }
    }
}
