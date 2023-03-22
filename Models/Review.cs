namespace Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int review_id { get; set; }

        public int? course_id { get; set; }

        [StringLength(128)]
        public string users_id { get; set; }

        public double? rating { get; set; }

        [Column(TypeName = "text")]
        public string comment { get; set; }

        public DateTime? review_date { get; set; }
    }
}
