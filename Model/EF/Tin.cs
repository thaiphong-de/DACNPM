namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tin")]
    public partial class Tin
    {
        [Key]
        [StringLength(5)]
        public string ma { get; set; }

        [StringLength(255)]
        public string tai_khoan { get; set; }

        [StringLength(4000)]
        public string noi_dung { get; set; }
    }
}
