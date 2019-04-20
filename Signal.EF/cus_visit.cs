namespace Signal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("virusecusvisit.cus_visit")]
    public partial class cus_visit
    {
        [Required]
        [StringLength(100)]
        public string VISIT_MAC { get; set; }

        [Column(TypeName = "date")]
        public DateTime? VISIT_TIME { get; set; }

        [StringLength(120)]
        public string CUS_NAME { get; set; }

        [StringLength(12)]
        public string CUS_MOBILE { get; set; }

        [StringLength(4)]
        public string CUS_SEX { get; set; }

        [StringLength(4)]
        public string CUS_AGE_RANGE { get; set; }

        [StringLength(2)]
        public string CUS_INFO_STATUS { get; set; }

        [Key]
        [StringLength(50)]
        public string Guid { get; set; }
    }
}
