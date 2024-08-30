using Microsoft.EntityFrameworkCore;
using StudentApi.Authorization;

namespace StudentApi.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");   
            modelBuilder.Entity<UserPermission>().ToTable("UserPermissions").HasKey(x=> new
            {
                x.UserId,
                x.PermissionId
            });   
        }

    }
}
