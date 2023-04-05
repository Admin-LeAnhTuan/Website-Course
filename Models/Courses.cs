namespace Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Courses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Courses()
        {
            Payments = new HashSet<Payment>();
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

        [StringLength(255)]
        public string duration { get; set; }

        [StringLength(128)]
        public string userid { get; set; }

        [StringLength(255)]
        public string img_course { get; set; }

        public int? category_id { get; set; }

        public int? level_id { get; set; }

        public DateTime? Course_date { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Category Category { get; set; }

        public virtual Level Level { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unit> Units { get; set; }


        public bool isBuyed = false;
        public int firstUnit_id = 1;
        public string priceAfterConvert()
        {
            string originalString = this.price;
            int length = originalString.Length;

            for (int i = length - 3; i > 0; i -= 3)
            {
                originalString = originalString.Insert(i, ",");
            }
            return originalString;
        }
    }
}
