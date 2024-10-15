using Microsoft.EntityFrameworkCore;
using StudentApi.Model.Student;
using StudentApi.Model.User;

namespace StudentApi.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _ = modelBuilder.Entity<User>().ToTable("Users");
            _ = modelBuilder.Entity<UserPermission>().ToTable("UserPermissions").HasKey(x => new
            {
                x.UserId,
                x.PermissionId
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbcontext).Assembly);
        }

    }
}
