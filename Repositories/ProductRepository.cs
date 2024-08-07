using LaptopCenter.Constraints;
using LaptopCenter.Data;
using LaptopCenter.DTO;
using LaptopCenter.Models;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace LaptopCenter.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductsDTO>> GetLatestProducts()
        {
            try
            {
                int getCurrentYear = DateTime.Now.Year;
                var products = await _context.Products
                    .Where(p => p.IsSale == true && p.CreateAt.Year == getCurrentYear)
                    .Join(
                        _context.Suppliers,
                        p => p.SupplierId,
                        s => s.SupplierId,
                        (p, s) => new { Product = p, Supplier = s })
                    .GroupJoin(
                        _context.Reviews,
                        ps => ps.Product.ProductId,
                        r => r.ProductId,
                        (ps, reviews) => new { ps.Product, ps.Supplier, Reviews = reviews })
                    .SelectMany(
                        psr => psr.Reviews.DefaultIfEmpty(),
                        (psr, review) => new { psr.Product, psr.Supplier, Review = review })
                    .GroupBy(
                        psr => new { psr.Product.ProductId, psr.Product.ProductName, psr.Product.ShortDescription, psr.Product.Image, psr.Product.Price, psr.Supplier.LogoUrl },
                        psr => psr.Review,
                        (key, reviews) => new ProductsDTO
                        {
                            ProductId = key.ProductId,
                            ProductName = key.ProductName,
                            ShortDescription = key.ShortDescription,
                            ProductImage = key.Image,
                            Price = key.Price,
                            Supplier = new Supplier { LogoUrl = key.LogoUrl },
                            AverageRate = reviews.Average(r => r == null ? 0 : r.Rate)
                        })
                    .Take(4)
                    .ToListAsync();

                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductsDTO> GetOneProduct(int productId)
        {
            try
            {
                var products = await _context.Products
                    .Where(p => p.ProductId == productId)
                    .Join(
                        _context.Suppliers,
                        p => p.SupplierId,
                        s => s.SupplierId,
                        (p, s) => new { Product = p, Supplier = s })
                    .GroupJoin(
                        _context.Reviews,
                        ps => ps.Product.ProductId,
                        r => r.ProductId,
                        (ps, reviews) => new { ps.Product, ps.Supplier, Reviews = reviews })
                    .SelectMany(
                        psr => psr.Reviews.DefaultIfEmpty(),
                        (psr, review) => new { psr.Product, psr.Supplier, Review = review })
                    .GroupBy(
                        psr => new {
                            psr.Product.ProductId,
                            psr.Product.ProductName,
                            psr.Product.ShortDescription,
                            psr.Product.DetailDescription,
                            psr.Product.Image,
                            psr.Product.Price,
                            psr.Supplier.SupplierName,
                            psr.Supplier.LogoUrl,
                            psr.Product.CPU,
                            psr.Product.RAM,
                            psr.Product.GraphicsCard,
                            psr.Product.ScreenSize,
                            psr.Product.WarrantyPeriod,
                            psr.Product.CreateAt,
                        },
                        psr => psr.Review,
                        (key, reviews) => new ProductsDTO
                        {
                            ProductId = key.ProductId,
                            ProductName = key.ProductName,
                            ShortDescription = key.ShortDescription,
                            DetailDescription = key.DetailDescription,
                            ProductImage = key.Image,
                            Price = key.Price,
                            Supplier = new Supplier { SupplierName = key.SupplierName, LogoUrl = key.LogoUrl },
                            CPU = key.CPU,
                            RAM = key.RAM,
                            GraphicsCard = key.GraphicsCard,
                            ScreenSize = key.ScreenSize,
                            WarrantyPeriod = key.WarrantyPeriod,
                            CreateAt = key.CreateAt,
                            AverageRate = reviews.Average(r => r == null ? 0 : r.Rate)
                        })
                    .FirstOrDefaultAsync();

                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET ALL Product in shop page

        public async Task<IEnumerable<ProductsDTO>> GetProducts(string status)
        {
            try
            {
                var query = _context.Products.AsQueryable();

                if (status == "getOnSale")
                {
                    query = query.Where(p => p.IsSale);
                }

                var productsQuery = query
                    .Join(
                        _context.Suppliers,
                        p => p.SupplierId,
                        s => s.SupplierId,
                        (p, s) => new { Product = p, Supplier = s })
                    .GroupJoin(
                        _context.Reviews,
                        ps => ps.Product.ProductId,
                        r => r.ProductId,
                        (ps, reviews) => new { ps.Product, ps.Supplier, Reviews = reviews })
                    .SelectMany(
                        psr => psr.Reviews.DefaultIfEmpty(),
                        (psr, review) => new { psr.Product, psr.Supplier, Review = review });

                if (status == "getOnSale")
                {
                    var products = await productsQuery
                        .GroupBy(
                            psr => new { psr.Product.ProductId, psr.Product.ProductName, psr.Product.ShortDescription, psr.Product.Image, psr.Product.Price, psr.Supplier.LogoUrl },
                            psr => psr.Review,
                            (key, reviews) => new ProductsDTO
                            {
                                ProductId = key.ProductId,
                                ProductName = key.ProductName,
                                ShortDescription = key.ShortDescription,
                                ProductImage = key.Image,
                                Price = key.Price,
                                Supplier = new Supplier { LogoUrl = key.LogoUrl },
                                AverageRate = reviews.Average(r => r == null ? 0 : r.Rate)
                            })
                        .ToListAsync();

                    return products;
                }
                else
                {
                    var products = await productsQuery
                        .GroupBy(
                            psr => new { psr.Product.ProductId, psr.Product.ProductName, psr.Product.ShortDescription, psr.Product.Image, psr.Product.Price, psr.Supplier.LogoUrl, psr.Product.DetailDescription, psr.Product.Quantity, psr.Product.IsSale, psr.Product.CPU, psr.Product.RAM, psr.Product.GraphicsCard, psr.Product.ScreenSize, psr.Product.WarrantyPeriod, psr.Product.CreateAt, CategoryName = psr.Product.Category.CategoryName, SupplierName = psr.Supplier.SupplierName },
                            psr => psr.Review,
                            (key, reviews) => new ProductsDTO
                            {
                                ProductId = key.ProductId,
                                ProductName = key.ProductName,
                                ShortDescription = key.ShortDescription,
                                DetailDescription = key.DetailDescription,
                                Quantity = key.Quantity,
                                IsSale = key.IsSale,
                                CPU = key.CPU,
                                RAM = key.RAM,
                                GraphicsCard = key.GraphicsCard,
                                ScreenSize = key.ScreenSize,
                                WarrantyPeriod = key.WarrantyPeriod,
                                CreateAt = key.CreateAt,
                                ProductImage = key.Image,
                                Price = key.Price,
                                Category = new Category { CategoryName = key.CategoryName},
                                Supplier = new Supplier { SupplierName = key.SupplierName, LogoUrl = key.LogoUrl },
                                AverageRate = reviews.Average(r => r == null ? 0 : r.Rate)
                            })
                        .ToListAsync();

                    return products;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<IEnumerable<ProductsDTO>> GetSuggestProducts(string supplierName)
        {
            try
            {
                var products = await _context.Products
                    .Where(p => p.IsSale == true && p.Supplier.SupplierName == supplierName)
                    .Join(
                        _context.Suppliers,
                        p => p.SupplierId,
                        s => s.SupplierId,
                        (p, s) => new { Product = p, Supplier = s })
                    .GroupJoin(
                        _context.Reviews,
                        ps => ps.Product.ProductId,
                        r => r.ProductId,
                        (ps, reviews) => new { ps.Product, ps.Supplier, Reviews = reviews })
                    .SelectMany(
                        psr => psr.Reviews.DefaultIfEmpty(),
                        (psr, review) => new { psr.Product, psr.Supplier, Review = review })
                    .GroupBy(
                        psr => new { psr.Product.ProductId, psr.Product.ProductName, psr.Product.ShortDescription, psr.Product.Image, psr.Product.Price, psr.Supplier.LogoUrl },
                        psr => psr.Review,
                        (key, reviews) => new ProductsDTO
                        {
                            ProductId = key.ProductId,
                            ProductName = key.ProductName,
                            ShortDescription = key.ShortDescription,
                            ProductImage = key.Image,
                            Price = key.Price,
                            Supplier = new Supplier { LogoUrl = key.LogoUrl },
                            AverageRate = reviews.Average(r => r == null ? 0 : r.Rate)
                        })
                    .ToListAsync();

                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductsDTO>> GetTopSellingProducts()
        {
            try
            {
                var currentDate = DateTime.Now;
                var orderStatus = EStatus.Confirmed; // Trạng thái cố định

                var topSellingProducts = await _context.OrderDetails
                .Where(od => od.Order.DeliveryDate.Year == currentDate.Year && od.Order.Status == orderStatus)
                .GroupBy(od => new
                {
                    od.Product.ProductId,
                    od.Product.ProductName,
                    od.Product.ShortDescription,
                    od.Product.Image,
                    od.Product.Price,
                    SupplierLogoUrl = od.Product.Supplier.LogoUrl
                })
                .Select(g => new
                {
                    g.Key.ProductId,
                    g.Key.ProductName,
                    g.Key.ShortDescription,
                    ProductImage = g.Key.Image,
                    g.Key.Price,
                    g.Key.SupplierLogoUrl,
                    TotalQuantitySold = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(g => g.TotalQuantitySold)
                .Take(6)
                .ToListAsync();

                var topSellingProductsWithRates = topSellingProducts.Select(g => new ProductsDTO
                {
                    ProductId = g.ProductId,
                    ProductName = g.ProductName,
                    ShortDescription = g.ShortDescription,
                    ProductImage = g.ProductImage,
                    Price = g.Price,
                    Supplier = new Supplier { LogoUrl = g.SupplierLogoUrl },
                    AverageRate = _context.Reviews
                        .Where(r => r.ProductId == g.ProductId)
                        .Average(r => (double?)r.Rate) ?? 0 // Avoid null values
                }).ToList();


                return topSellingProductsWithRates;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string SaveProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return "Product saved successfully.";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
