namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string tai_khoan { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string ma_san_pham { get; set; }

        [StringLength(255)]
        public string noi_dung { get; set; }
    }
}
