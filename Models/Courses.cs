namespace Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Course")]
    public partial class Courses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Courses()
        {
            Reviews = new HashSet<Review>();
            Units = new HashSet<Unit>();
        }

        [Key]
        public int course_id { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string price { get; set; }

        public TimeSpan? duration { get; set; }

        [StringLength(128)]
        public string userid { get; set; }

        [StringLength(255)]
        public string img_course { get; set; }

        public int? category_id { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unit> Units { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}
