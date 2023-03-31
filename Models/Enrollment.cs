namespace Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enrollment")]
    public partial class Enrollment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Enrollment()
        {
            Payments = new HashSet<Payment>();
        }

        [Key]
        public int enrollment_id { get; set; }

        public int? course_id { get; set; }

        [StringLength(128)]
        public string users_id { get; set; }

        public DateTime? enrollment_date { get; set; }

        public int? statuss { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
