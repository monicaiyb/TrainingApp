
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Data.Models.Users;


namespace TrainingApp.Data.SeedData
{
    public class SeedData
    {
        private readonly ModelBuilder modelBuilder;
        private static readonly TrainingContextDb _dbContext;

       


        public static async Task SeedDataAndRole(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            
            await SeedRoles(roleManager);
            await SeedUserAndRoles(userManager);
           
        }
        public static async Task SeedUserAndRoles(UserManager<ApplicationUser> userManager)  
        {
            try
            {

            
            string adminUserEmail = "monicaiyb@gmail.com";
            string sisterUserEmail = "monicaiyb@yahoo.com";
            string normalUserEmail = "monicaiyb+1@gmail.com";

                var adminUser =  userManager.FindByEmailAsync(adminUserEmail).Result;
            if (adminUser == null)
            {

                var newAdminUser = new ApplicationUser()
                {
                    Email = adminUserEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true,
                    Id = Guid.NewGuid(),
                    UserName = adminUserEmail,
                    PhoneNumber = "0785004848",
                    Password = "Potato@123"
                };
                var result =  userManager.CreateAsync(newAdminUser, newAdminUser.Password).Result;

                    if (result.Succeeded)
                    {

                        var roleResult = userManager.AddToRoleAsync(newAdminUser, "AdminUser").Result;
                    }
                }
            var normalUser = userManager.FindByEmailAsync(normalUserEmail).Result;
            if (normalUser == null)
            {

                var newNormalUser = new ApplicationUser()
                {

                    Email = normalUserEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true,
                    Id = Guid.NewGuid(),
                    UserName = normalUserEmail,
                    PhoneNumber = "0785004848",
                    Password = "Password@123"
                };
                var result = userManager.CreateAsync(newNormalUser, newNormalUser.Password).Result;

                if (result.Succeeded)
                {
                    var roleResult = userManager.AddToRoleAsync(newNormalUser, "NormalUser").Result;
                }
            }
            var sisterUser = userManager.FindByEmailAsync(sisterUserEmail).Result;
            if (sisterUser == null)
            {

                var newSisterUserlUser = new ApplicationUser()
                {

                    Email = sisterUserEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true,
                    Id = Guid.NewGuid(),
                    UserName = sisterUserEmail,
                    PhoneNumber = "0785004848",
                    Password = "Password@123"
                };
                var result = userManager.CreateAsync(newSisterUserlUser, newSisterUserlUser.Password).Result;

                if (result.Succeeded)
                {
                    var roleResult = userManager.AddToRoleAsync(newSisterUserlUser, "AdminUser").Result;
                }
            }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        private static  async Task SeedRoles(RoleManager<Role> roleManager)
        {
            var roleNormal = roleManager.RoleExistsAsync("NormalUser").Result;
            var roleAdmin = roleManager.RoleExistsAsync("AdminUser").Result;

            if (!roleNormal)
                {
                    Role role = new Role("NormalUser");
                    role.Id = Guid.NewGuid();

                    //var roleResult = await roleManager.CreateAsync(role).Result;
                    IdentityResult roleResult = roleManager.CreateAsync(role).GetAwaiter().GetResult();


                    if (!roleResult.Succeeded)
                    {
                        throw new Exception($"Default Role for Creation Error(s): {string.Join(",", roleResult.Errors.Select(r => $"{r.Code}: {r.Description}"))}");
                    }

            }

            if (!roleAdmin)
            {
                Role role = new Role("AdminUser");
                    
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                if (!roleResult.Succeeded)
                {
                    throw new Exception($"Default Role for Admin User Creation Error(s): {string.Join(",", roleResult.Errors.Select(r => $"{r.Code}: {r.Description}"))}");
                }
            }

        }
    }
}
