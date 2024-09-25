using Microsoft.EntityFrameworkCore;
using MyLearn.DataLayer.Entities.Course;
using MyLearn.DataLayer.Entities.Forum;
using MyLearn.DataLayer.Entities.Order;
using MyLearn.DataLayer.Entities.Permission;
using MyLearn.DataLayer.Entities.User;
using MyLearn.DataLayer.Entities.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Context
{
    public class MyLearnContext:DbContext
    {
        public MyLearnContext(DbContextOptions<MyLearnContext> options):base(options)
        {
                
        }

        #region DbSets
        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        #endregion
        #region Permission
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        #endregion
        #region Course
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }
        #endregion
        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<UserDiscount> UserDiscounts { get; set; }
        #endregion
        #region Forum
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        #endregion
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            //modelBuilder.Entity<Course>()
            //   .HasOne<CourseGroup>(f => f.CourseGroup)
            //   .WithMany(g => g.Courses)
            //   .HasForeignKey(f => f.GroupId);

            //modelBuilder.Entity<Course>()
            //    .HasOne<CourseGroup>(f => f.SubGroup)
            //    .WithMany(g => g.SubGroup)
            //    .HasForeignKey(f => f.SubGroupId);


            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            base.OnModelCreating(modelBuilder);
        }
    }
}
