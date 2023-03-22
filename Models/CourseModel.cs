using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Course.Models
{
    public partial class CourseModel : DbContext
    {
        public CourseModel()
            : base("name=CourseModel")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.comment)
                .IsUnicode(false);
        }
    }
}
