using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace databasr2.Data
{
    public class SampleData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            string[] roles = new string[] { "Administrator", "User" };

            foreach (var role in roles)
            {
                var isExist = await roleManager.RoleExistsAsync(role);
                if (!isExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var adminUser = new IdentityUser
            {
                Email = "pchrpachara@gmail.com",
                UserName = "pchrpachara@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var currentUser = await userManager.FindByEmailAsync(adminUser.Email);
            if (currentUser == null)
            {
                await userManager.CreateAsync(adminUser, "Secret123!");
                currentUser = await userManager.FindByEmailAsync(adminUser.Email);
            }
            var isAdmin = await userManager.IsInRoleAsync(currentUser, "Administrator");
            if (!isAdmin)
            {
               await userManager.AddToRolesAsync(currentUser, roles);
            }
            var containSampleBook = await dbContext.books.AnyAsync(b => b.Name == "Sample Book");
            if (!containSampleBook)
            {
                dbContext.books.Add(new Models.book
                {
                    Name = "Semple Book",
                    Price = 100m
                });
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
