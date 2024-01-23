using EcommerceLib.Models.ProductModel;
using EcommerceLib.Models.UserModel.JWT;
using EcommerceLib.Models.UserModel.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EcommerceLib.Context;

public class DeviceDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<ProductСharacteristic> ProductChars { get; set; }
    public DbSet<BrandAndSubCategory> BrandAndSubCategories { get; set; }
    public DbSet<ProductsImg> ProductImages { get; set; }
    public DbSet<ProductOriginalImg> ProductOriginalImages { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<UserLikedProduct> UserLikedProducts { get; set; }
    public DbSet<PurchasedProduct> PurchasedProducts { get; set; }

    public DeviceDbContext() { }
    public DeviceDbContext(DbContextOptions<DeviceDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddUserSecrets<DeviceDbContext>().Build();
        var connectionString = config["DbConnection:DeviceDbConnection"];
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var subCategory = modelBuilder.Entity<SubCategory>();
        var category = modelBuilder.Entity<Category>();
        var product = modelBuilder.Entity<Product>();
        var brand = modelBuilder.Entity<Brand>();
        var characteristics = modelBuilder.Entity<Characteristic>();
        var productChar = modelBuilder.Entity<ProductСharacteristic>();
        var brandSubCategory = modelBuilder.Entity<BrandAndSubCategory>();
        var imgs = modelBuilder.Entity<ProductsImg>();
        var originalImgs = modelBuilder.Entity<ProductOriginalImg>();
        var reviews = modelBuilder.Entity<Review>();
        var userProducts = modelBuilder.Entity<UserLikedProduct>();
        var purchusedProducts = modelBuilder.Entity<PurchasedProduct>();
        modelBuilder.Ignore<AppUser>();
        modelBuilder.Entity<AppUser>().ToTable("AspNetUsers");

        ////**********************************************************************

        characteristics.HasKey(c => c.Id);
        characteristics.Property(c => c.Name).IsRequired();
        characteristics.Property(x => x.Description).IsRequired();

        ////**********************************************************************

        subCategory.HasKey(c => c.Id);
        subCategory.Property(c => c.Name).IsRequired();

        subCategory
            .HasMany(x => x.Characteristics)
            .WithOne(x => x.SubCategory)
            .HasForeignKey(x => x.SubCategoryId);

        subCategory
            .HasMany(x => x.Products)
            .WithOne(x => x.SubCategory)
            .HasForeignKey(x => x.SubCategoryId);

        ////**********************************************************************

        category.HasKey(d => d.Id);
        category.Property(d => d.Name).IsRequired(true);

        category
            .HasMany(c => c.SubCategories)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);

        category.HasIndex(x => x.Name).IsUnique();

        ////**********************************************************************

        brand.HasKey(c => c.Id);
        brand.Property(c => c.Name).IsRequired();

        ////**********************************************************************

        imgs.HasKey(x => x.Id);
        imgs.HasOne(x => x.Product)
            .WithMany(x => x.ProductsImg)
            .HasForeignKey(x => x.ProductId);

        ////**********************************************************************

        originalImgs.HasKey(x => x.Id);

        originalImgs.HasOne(x => x.Product)
          .WithMany(x => x.OriginalImgs)
          .HasForeignKey(x => x.ProductId);

        ////**********************************************************************
        product.HasKey(d => d.Id);

        product.Property(d => d.Name).IsRequired();
        product.Property(d => d.Price).IsRequired();
        product.Ignore(x => x.Count);

        product
            .HasMany(x => x.ProductСharacteristics)
            .WithOne(db => db.Product)
            .HasForeignKey(x => x.ProductId);

        ////**********************************************************************

        productChar.HasKey(x => x.Id);

        productChar.HasOne(x => x.Product)
                   .WithMany(x => x.ProductСharacteristics)
                   .HasForeignKey(x => x.ProductId);

        ////**********************************************************************
        reviews.HasKey(x => x.Id);

        reviews.HasOne(x => x.Product)
               .WithMany(x => x.Reviews)
               .HasForeignKey(x => x.ProductId);

        reviews.HasOne(x => x.AppUser)
              .WithMany(x => x.Reviews)
              .HasForeignKey(x => x.AppUserId);

        ////**********************************************************************

        brandSubCategory.HasKey(x => x.Id);

        brandSubCategory
            .HasOne(x => x.Brand)
            .WithMany(x => x.BrandAndSubCategories)
            .HasForeignKey(x => x.BrandId);

        brandSubCategory
           .HasOne(x => x.SubCategory)
           .WithMany(x => x.BrandAndSubCategories)
           .HasForeignKey(x => x.SubCategoryId);

        ////**********************************************************************

        userProducts.HasKey(x => x.Id);

        userProducts.HasOne(x => x.AppUser)
            .WithMany(x => x.FavoriteProducts)
            .HasForeignKey(x => x.AppUserId);

        userProducts.HasOne(x => x.Product)
            .WithMany(x => x.UserProducts)
            .HasForeignKey(x => x.ProductId);

        ////**********************************************************************

        purchusedProducts.HasKey(x => x.Id);
        purchusedProducts.HasOne(x => x.AppUser)
                         .WithMany(x => x.PurchasedProducts)
                         .HasForeignKey(x => x.AppUserId);


        purchusedProducts.HasOne(x => x.Product)
                        .WithMany(x => x.PurchasedProducts)
                        .HasForeignKey(x => x.ProductId);

    }
}
