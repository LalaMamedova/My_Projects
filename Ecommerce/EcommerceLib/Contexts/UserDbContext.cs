using EcommerceLib.Models.ProductModel;
using EcommerceLib.Models.UserModel.JWT;
using EcommerceLib.Models.UserModel.Users;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace EcommerceLib.Context;
public class UserDbContext : IdentityDbContext<AppUser>
{

    public UserDbContext(){}
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) =>   Database.EnsureCreated();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddUserSecrets<UserDbContext>().Build();
        var connectionString = config["DbConnection:DeviceDbConnection"];
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Review>().ToTable("Reviews");
        modelBuilder.Entity<UserLikedProduct>().ToTable("UserLikedProducts");
        modelBuilder.Entity<PurchasedProduct>().ToTable("PurchasedProducts");
        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<ProductsImg>().ToTable("ProductImages");
        modelBuilder.Entity<Product>().Ignore(x=>x.Count);

    }
}
