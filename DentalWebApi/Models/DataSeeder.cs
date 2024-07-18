using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalWebApi.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DentalWebApi.Models
{
    public class DataSeeder
    {
        private readonly DentalContext _dentalContext;

        public DataSeeder(DentalContext dentalContext)
        {
            _dentalContext = dentalContext;
        }
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new DentalContext(serviceProvider.GetRequiredService<DbContextOptions<DentalContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything
                //Todo Change Emails instead of @contoso.com 

                var superAdminID = await EnsureUser(serviceProvider, testUserPw, "superadmin@contoso.com");
                await EnsureRole(serviceProvider, superAdminID, Roles.SuperAdmin.ToString());

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Roles.Admin.ToString());

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Roles.Manager.ToString());

                // Seed(context, adminID);
            }
        }

        private static async Task<int> EnsureUser(IServiceProvider serviceProvider, string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();

            var user = await userManager.FindByNameAsync(UserName);
            // var email = await userManager.FindByEmailAsync(UserName);
            if (user == null)
            {
                user = new User
                {
                    // Id = 1,
                    UserName = UserName,
                    Email = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, int uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid.ToString());

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        // public void Seed()
        // {
        //     // var userManager = ServiceProvider.GetService<UserManager<User>>();
        //     if(!_dentalContext.Users.Any())
        //     {
        //         var user = new User
        //         {
        //             Name = "Admin",
        //             Email = "admin@admin.com",
        //             PhoneNumber = "0123456789",
        //         };
        //         // await userManager.CreateAsync(user, testUserPw);
        //     }
        // }
    }
}