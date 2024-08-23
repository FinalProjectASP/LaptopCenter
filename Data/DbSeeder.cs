using LaptopCenter.Constraints;
using LaptopCenter.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace LaptopCenter.Data
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<AppUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(ERole.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ERole.User.ToString()));

            // creating admin

            var admin = new AppUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FullName = "Admin",
                Gender = true,
                Birthday = DateTime.Now,
                Address = "Admin",
                Telephone = "0987654321",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var adminInDb = await userManager.FindByEmailAsync(admin.Email);
            if (adminInDb == null)
            {
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, ERole.Admin.ToString());
            }
        }
    }
}
