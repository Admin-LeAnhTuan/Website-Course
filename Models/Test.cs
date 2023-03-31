namespace Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Test")]
    public partial class Test
    {
        [Key]
        public int Test_id { get; set; }

        public int? Test_unit_id { get; set; }

        [StringLength(255)]
        public string Test_case { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
