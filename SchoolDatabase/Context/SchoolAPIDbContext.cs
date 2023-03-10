using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Model.Entity.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SchoolDatabase.Context
{
    public class SchoolAPIDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int> // DbContext
    {

        //public DbSet<Teacher> Teachers { get; set; }
        //public DbSet<Student> Students { get; set; }
        //public DbSet<Semester> Semesters { get; set; }
        //public DbSet<Subject> Subjects { get; set; }
        //public DbSet<Course> Course { get; set; }
        //public DbSet<Position> Positions { get; set; }
        //public DbSet<Department> Departments { get; set; }
        //public DbSet<Speciality> Specialitys { get; set; }

        public SchoolAPIDbContext(DbContextOptions<SchoolAPIDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(60);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.RemovePluralizingTableNameConvention();
            modelBuilder.RemoveOneToManyCascadeDeleteConvention();


            /// <summary>
            /// Global query filter for the entities (related to task 7)
            /// </summary>
            modelBuilder.Entity<Student>().HasQueryFilter(rs => !rs.Deleted);
            modelBuilder.Entity<Teacher>().HasQueryFilter(rs => !rs.Deleted);
            modelBuilder.Entity<Subject>().HasQueryFilter(rs => !rs.Deleted);
            modelBuilder.Entity<Course>().HasQueryFilter(rs => !rs.Deleted);
            modelBuilder.Entity<Semester>().HasQueryFilter(rs => !rs.Deleted);

            /// default value for Course timetable informations
            modelBuilder.Entity<Course>().Property(s => s.Timetable).HasDefaultValue("Ismeretlen");

            base.OnModelCreating(modelBuilder);
        }
    }

    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Define the name of the table to be the name of the entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.BaseType == null && !HasAttribute(entity.ClrType, typeof(TableAttribute)))
                {
                    entity.SetTableName(entity.DisplayName());
                }
            }
        }

        /// <summary>
        /// Disable cascade deletion for one or more connections
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void RemoveOneToManyCascadeDeleteConvention(this ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        private static bool HasAttribute(Type type, Type attributeType)
        {
            return type.GetCustomAttribute(attributeType) != null;
        }
    }
}
