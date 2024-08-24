using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaptopCenter.Data;
using LaptopCenter.Models;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.CodeAnalysis;
using LaptopCenter.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LaptopCenter.Controllers
{
    [Authorize(Roles = "User")]
    public class ReviewsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(UserManager<AppUser> userManager, IReviewRepository reviewRepository)
        {
            _userManager = userManager;
            _reviewRepository = reviewRepository;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            List<Review> reviews = _reviewRepository.GetReviews();
            return View(reviews);
        }

        // GET: Reviews/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _reviewRepository.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create(int productId, int orderId)
        {
            ViewBag.ProductId = productId;
            ViewBag.OrderId = orderId;
            return View();
        }

        [HttpPost]
        public IActionResult CreateReview([FromForm] Review reviewObj)
        {
            // Validate inputs
            if (reviewObj.Rate < 1 || reviewObj.Rate > 5)
            {
                ModelState.AddModelError("", "Invalid input");
                return RedirectToAction("Create"); // return the view with validation errors
            }

            string userId = _userManager.GetUserId(User);

            reviewObj.Date = DateTime.Now;
            reviewObj.UserId = userId;

            if (reviewObj != null)
            {
                _reviewRepository.SaveReview(reviewObj);
                return LocalRedirect("~/Identity/Account/Manage");
            }

            return Json(new { success = false, message = "Review item add failt." });
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", review.ProductId);
            return View(review);*/
            return View();
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,Rate,ProductId")] Review review)
        {
            /*if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    review.Date = DateTime.Now;
                    review.UserId = "trannq";
                    _reviewRepository.UpdateReview(review);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", review.ProductId);
            return View(review);*/

            return View();
        }

        // GET: Reviews/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _reviewRepository.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = _reviewRepository.GetReviewById(id);
            if (review != null)
            {
                _reviewRepository.RemoveReview(review);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
