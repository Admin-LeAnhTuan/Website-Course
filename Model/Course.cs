namespace Course.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int course_id { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        [StringLength(255)]
        public string price { get; set; }

        [StringLength(255)]
        public string duration { get; set; }

        [StringLength(128)]
        public string userid { get; set; }

        public int? category_id { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Category Category { get; set; }
    }
}
