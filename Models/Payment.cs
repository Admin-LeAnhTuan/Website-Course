namespace Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        [Key]
        public int payment_id { get; set; }

        public int? enrollment_id { get; set; }

        public DateTime? payment_date { get; set; }

        public double? amount { get; set; }

        public virtual Enrollment Enrollment { get; set; }
    }
}
