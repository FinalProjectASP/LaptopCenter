using LaptopCenter.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace LaptopCenter.Data
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
	{
		public ApplicationDbContext()
		{
		}

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			try
			{
				if (Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator)
				{
					if (!databaseCreator.CanConnect())
					{
						databaseCreator.Create();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<OrderDetail>()
				.HasKey(o => new { o.OrderId, o.ProductId });

			modelBuilder.Entity<Cart>()
				.HasKey(o => new { o.UserId, o.ProductId });

			modelBuilder.Entity<Product>()
				.HasOne(p => p.Supplier)
				.WithMany(s => s.Products)
				.HasForeignKey(p => p.SupplierId);

			// Seed Suppliers
			modelBuilder.Entity<Supplier>().HasData(
				new Supplier { SupplierId = 1, SupplierName = "MSI", LogoUrl = "laptop-msi.jpg" },
				new Supplier { SupplierId = 2, SupplierName = "Acer", LogoUrl = "laptop-accer.png" },
				new Supplier { SupplierId = 3, SupplierName = "Microsoft", LogoUrl = "laptop-microsoft.png" },
				new Supplier { SupplierId = 4, SupplierName = "HP", LogoUrl = "laptop-hp.png" },
				new Supplier { SupplierId = 5, SupplierName = "Microsoft", LogoUrl = "laptop-microsoft.png" }
			);

			// Seed categories
			modelBuilder.Entity<Category>().HasData(
				new Category { CategoryId = 1, CategoryName = "Electronics" },
				new Category { CategoryId = 2, CategoryName = "Computers" },
				new Category { CategoryId = 3, CategoryName = "Smartphones" }
			);

			// Seed products
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 1,
					ProductName = "Samsung Galaxy S21",
					ShortDescription = "Latest Samsung smartphone",
					DetailDescription = "Full details about Samsung Galaxy S21",
					Image = "laptop-msi.jpg",
					IsSale = true,
					Quantity = 50,
					CPU = "Exynos 2100",
					RAM = "8GB",
					GraphicsCard = "128GB",
					ScreenSize = "6.2 inches",
					WarrantyPeriod = "2 years",
					CategoryId = 3,
					SupplierId = 1,
					Price = 799.99m,
					CreateAt = new DateTime(2022, 1, 1)
				},
				new Product
				{
					ProductId = 2,
					ProductName = "Apple MacBook Pro",
					ShortDescription = "High-performance laptop",
					DetailDescription = "Full details about Apple MacBook Pro",
					Image = "laptop-microsoft.png",
					IsSale = true,
					Quantity = 30,
					CPU = "M1",
					RAM = "16GB",
					GraphicsCard = "512GB",
					ScreenSize = "13 inches",
					WarrantyPeriod = "1 year",
					CategoryId = 2,
					SupplierId = 2,
					Price = 1299.99m,
					CreateAt = new DateTime(2022, 2, 1)
				},
				new Product
				{
					ProductId = 3,
					ProductName = "Dell XPS 13",
					ShortDescription = "Compact and powerful laptop",
					DetailDescription = "Full details about Dell XPS 13",
					Image = "laptop-accer.png",
					IsSale = false,
					Quantity = 20,
					CPU = "Intel i7",
					RAM = "16GB",
					GraphicsCard = "1TB",
					ScreenSize = "13.4 inches",
					WarrantyPeriod = "1 year",
					CategoryId = 2,
					SupplierId = 3,
					Price = 999.99m,
					CreateAt = new DateTime(2022, 3, 1)
				},
				new Product
				{
					ProductId = 4,
					ProductName = "HP Spectre x360",
					ShortDescription = "Convertible laptop",
					DetailDescription = "Full details about HP Spectre x360",
					Image = "laptop-hp.jpg",
					IsSale = true,
					Quantity = 25,
					CPU = "Intel i7",
					RAM = "16GB",
					GraphicsCard = "1TB",
					ScreenSize = "13.3 inches",
					WarrantyPeriod = "1 year",
					CategoryId = 2,
					SupplierId = 4,
					Price = 1099.99m,
					CreateAt = new DateTime(2022, 4, 1)
				},
				new Product
				{
					ProductId = 5,
					ProductName = "Lenovo ThinkPad X1 Carbon",
					ShortDescription = "Business laptop",
					DetailDescription = "Full details about Lenovo ThinkPad X1 Carbon",
					Image = "laptop-microsoft.png",
					IsSale = true,
					Quantity = 15,
					CPU = "Intel i7",
					RAM = "16GB",
					GraphicsCard = "512GB",
					ScreenSize = "14 inches",
					WarrantyPeriod = "3 years",
					CategoryId = 2,
					SupplierId = 5,
					Price = 1499.99m,
					CreateAt = new DateTime(2022, 5, 1)
				},
				new Product
				{
					ProductId = 6,
					ProductName = "Samsung Galaxy Tab S7",
					ShortDescription = "High-end tablet",
					DetailDescription = "Full details about Samsung Galaxy Tab S7",
					Image = "laptop-hp.jpg",
					IsSale = false,
					Quantity = 40,
					CPU = "Snapdragon 865+",
					RAM = "6GB",
					GraphicsCard = "128GB",
					ScreenSize = "11 inches",
					WarrantyPeriod = "1 year",
					CategoryId = 1,
					SupplierId = 1,
					Price = 649.99m,
					CreateAt = new DateTime(2022, 6, 1)
				},
				new Product
				{
					ProductId = 7,
					ProductName = "Apple iPhone 13",
					ShortDescription = "Latest Apple smartphone",
					DetailDescription = "Full details about Apple iPhone 13",
					Image = "laptop-accer.png",
					IsSale = true,
					Quantity = 100,
					CPU = "A15 Bionic",
					RAM = "4GB",
					GraphicsCard = "128GB",
					ScreenSize = "6.1 inches",
					WarrantyPeriod = "1 year",
					CategoryId = 3,
					SupplierId = 2,
					Price = 599.99m,
					CreateAt = new DateTime(2022, 7, 1)
				},
				new Product
				{
					ProductId = 8,
					ProductName = "Dell Inspiron 15",
					ShortDescription = "Affordable laptop",
					DetailDescription = "Full details about Dell Inspiron 15",
					Image = "laptop-hp.jpg",
					IsSale = false,
					Quantity = 10,
					CPU = "Intel i5",
					RAM = "8GB",
					GraphicsCard = "256GB",
					ScreenSize = "15.6 inches",
					WarrantyPeriod = "1 year",
					CategoryId = 2,
					SupplierId = 3,
					Price = 399.99m,
					CreateAt = new DateTime(2022, 8, 1)
				},
				new Product
				{
					ProductId = 9,
					ProductName = "HP Envy 13",
					ShortDescription = "Portable laptop",
					DetailDescription = "Full details about HP Envy 13",
					Image = "laptop-microsoft.png",
					IsSale = true,
					Quantity = 35,
					CPU = "Intel i7",
					RAM = "16GB",
					GraphicsCard = "512GB",
					ScreenSize = "13.3 inches",
					WarrantyPeriod = "1 year",
					CategoryId = 2,
					SupplierId = 4,
					Price = 899.99m,
					CreateAt = new DateTime(2022, 9, 1)
				},
				new Product
				{
					 ProductId = 10,
					ProductName = "Lenovo Yoga 7i",
					ShortDescription = "Convertible laptop",
					DetailDescription = "Full details about Lenovo Yoga 7i",
					Image = "laptop-microsoft.png",
					IsSale = true,
					Quantity = 20,
					CPU = "Intel i5",
					RAM = "8GB",
					GraphicsCard = "256GB",
					ScreenSize = "14 inches",
					WarrantyPeriod = "1 year",
					CategoryId = 2,
					SupplierId = 5,
					Price = 799.99m,
					CreateAt = new DateTime(2022, 10, 1)
				}
			);
		}
	}
}
