// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LaptopCenter.DTO;
using LaptopCenter.Models;
using LaptopCenter.Repositories;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LaptopCenter.Areas.Identity.Pages.Account.Manage
{
    public class PurchaseHistoryModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderRepository _orderRepository;

        public PurchaseHistoryModel(
            UserManager<AppUser> userManager,
            IOrderRepository orderRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public List<OrderDTO>? PurchaseHistory { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            string userId = _userManager.GetUserId(User);
            PurchaseHistory = _orderRepository.GetHistoryPurchase(userId) ?? new List<OrderDTO>();

            return Page();
        }
    }
}
