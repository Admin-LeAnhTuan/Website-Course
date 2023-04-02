namespace Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoursePayment")]
    public partial class CoursePayment
    {
        [Key]
        public int Payment_id { get; set; }

        public int? enrollment_id { get; set; }

        public DateTime? Payment_date { get; set; }

        public double? amount { get; set; }

        public virtual Enrollment Enrollment { get; set; }
    }
}
