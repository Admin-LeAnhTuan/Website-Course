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
        public int Id { get; set; }

        public long? Order_Id { get; set; }

        public DateTime? payment_date { get; set; }

        public int? price { get; set; }

        [StringLength(128)]
        public string users_id { get; set; }

        public int? course_id { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Courses Course { get; set; }
    }
}
