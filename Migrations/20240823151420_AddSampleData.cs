using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LaptopCenter.Migrations
{
    /// <inheritdoc />
    public partial class AddSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Computers" },
                    { 3, "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "LogoUrl", "SupplierName" },
                values: new object[,]
                {
                    { 1, "laptop-msi.jpg", "MSI" },
                    { 2, "laptop-accer.png", "Acer" },
                    { 3, "laptop-microsoft.png", "Microsoft" },
                    { 4, "laptop-hp.png", "HP" },
                    { 5, "laptop-microsoft.png", "Microsoft" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CPU", "CategoryId", "CreateAt", "DetailDescription", "GraphicsCard", "Image", "IsSale", "Price", "ProductName", "Quantity", "RAM", "ScreenSize", "ShortDescription", "SupplierId", "WarrantyPeriod" },
                values: new object[,]
                {
                    { 1, "Exynos 2100", 3, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full details about Samsung Galaxy S21", "128GB", "laptop-msi.jpg", true, 799.99m, "Samsung Galaxy S21", 50, "8GB", "6.2 inches", "Latest Samsung smartphone", 1, "2 years" },
                    { 2, "M1", 2, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full details about Apple MacBook Pro", "512GB", "laptop-microsoft.png", true, 1299.99m, "Apple MacBook Pro", 30, "16GB", "13 inches", "High-performance laptop", 2, "1 year" },
                    { 3, "Intel i7", 2, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full details about Dell XPS 13", "1TB", "laptop-accer.png", false, 999.99m, "Dell XPS 13", 20, "16GB", "13.4 inches", "Compact and powerful laptop", 3, "1 year" },
                    { 4, "Intel i7", 2, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full details about HP Spectre x360", "1TB", "laptop-hp.jpg", true, 1099.99m, "HP Spectre x360", 25, "16GB", "13.3 inches", "Convertible laptop", 4, "1 year" },
                    { 5, "Intel i7", 2, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full details about Lenovo ThinkPad X1 Carbon", "512GB", "laptop-microsoft.png", true, 1499.99m, "Lenovo ThinkPad X1 Carbon", 15, "16GB", "14 inches", "Business laptop", 5, "3 years" },
                    { 6, "Snapdragon 865+", 1, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full details about Samsung Galaxy Tab S7", "128GB", "laptop-hp.jpg", false, 649.99m, "Samsung Galaxy Tab S7", 40, "6GB", "11 inches", "High-end tablet", 1, "1 year" },
                    { 7, "A15 Bionic", 3, new DateTime(2022, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full details about Apple iPhone 13", "128GB", "laptop-accer.png", true, 599.99m, "Apple iPhone 13", 100, "4GB", "6.1 inches", "Latest Apple smartphone", 2, "1 year" },
                    { 8, "Intel i5", 2, new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full details about Dell Inspiron 15", "256GB", "laptop-hp.jpg", false, 399.99m, "Dell Inspiron 15", 10, "8GB", "15.6 inches", "Affordable laptop", 3, "1 year" },
                    { 9, "Intel i7", 2, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full details about HP Envy 13", "512GB", "laptop-microsoft.png", true, 899.99m, "HP Envy 13", 35, "16GB", "13.3 inches", "Portable laptop", 4, "1 year" },
                    { 10, "Intel i5", 2, new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full details about Lenovo Yoga 7i", "256GB", "laptop-microsoft.png", true, 799.99m, "Lenovo Yoga 7i", 20, "8GB", "14 inches", "Convertible laptop", 5, "1 year" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 5);
        }
    }
}
