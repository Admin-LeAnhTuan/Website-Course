using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Course.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
            : base("name=ModelContext")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<CoursePayment> CoursePayments { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Courses)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.userid);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Enrollments)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.users_id);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Reviews)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.users_id);

            modelBuilder.Entity<Review>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Tests)
                .WithOptional(e => e.Unit)
                .HasForeignKey(e => e.Test_unit_id);
        }
    }
}
