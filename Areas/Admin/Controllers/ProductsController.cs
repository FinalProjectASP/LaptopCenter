using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaptopCenter.Data;
using LaptopCenter.Models;
using LaptopCenter.DTO;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using LaptopCenter.Services;

namespace LaptopCenter.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;


        public ProductsController(
            ApplicationDbContext context,
            IConfiguration configuration,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ISupplierRepository supplierRepository,
            IFileService fileService
        )
        {
            _context = context;
            _configuration = configuration;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _fileService = fileService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            List<ProductsDTO> products = (List<ProductsDTO>)await _productRepository.GetProducts("getAll");
            return View(products);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetCategories();
            var suppliers = await _supplierRepository.GetSuppliers();

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Suppliers = new SelectList(suppliers, "SupplierId", "SupplierName");

            return View();
        }


        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                var result = _fileService.SaveImage(product.ImageFile, Constraints.EFileType.Product);
                if (result.Item1 == 1)
                {
                    product.Image = result.Item2;
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            var categories = await _categoryRepository.GetCategories();
            var suppliers = await _supplierRepository.GetSuppliers();

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Suppliers = new SelectList(suppliers, "SupplierId", "SupplierName");

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await _categoryRepository.GetCategories();
            var suppliers = await _supplierRepository.GetSuppliers();

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Suppliers = new SelectList(suppliers, "SupplierId", "SupplierName");

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ImageFile != null)
                    {
                        var result = _fileService.SaveImage(product.ImageFile, Constraints.EFileType.Product);
                        if (result.Item1 == 1)
                        {
                            var oldImage = product.Image;
                            product.Image = result.Item2;

                            if (!string.IsNullOrEmpty(oldImage))
                            {
                                _fileService.DeleteImage(oldImage, Constraints.EFileType.Product);
                            }
                        }
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetCategories();
            var suppliers = await _supplierRepository.GetSuppliers();

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Suppliers = new SelectList(suppliers, "SupplierId", "SupplierName");

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
