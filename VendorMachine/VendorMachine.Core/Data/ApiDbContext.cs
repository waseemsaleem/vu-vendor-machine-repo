#nullable disable

using Microsoft.EntityFrameworkCore;
using VendorMachine.Core.Models;

namespace VendorMachine.Core.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserModel>(options =>
            {
                options.HasKey(x => x.UserId);
                options.ToTable("Users");
            });
            builder.Entity<RoleModel>(options =>
            {
                options.HasKey(x => x.RoleId);
                options.ToTable("Roles");
            });
            builder.Entity<UserRoleModel>(options =>
            {
                options.HasOne(x => x.User).WithMany(x=>x.UserRoles).HasForeignKey(x => x.UserId);
                options.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
                options.ToTable("UserRoles");
            });
            builder.Entity<ProductModel>(options =>
            {
                options.HasKey(x => x.ProductId);
                options.ToTable("Products");
            });
        }
    }
}
