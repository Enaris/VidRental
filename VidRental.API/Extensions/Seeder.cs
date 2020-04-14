using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidRental.DataAccess.DataContext;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Roles;

namespace VidRental.API.Extensions
{
    public static class Seeder
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (rolesManager.Roles.Any())
                    return;

                var roles = ApiRoles.IdentityRoles;

                foreach(var r in roles)
                {
                    var roleAdded = await rolesManager.CreateAsync(r);
                    if (!roleAdded.Succeeded)
                        throw new Exception("Roles initialization failed");
                }

                var usersManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var admin = new User 
                { 
                    UserName = "admin@vidRental.com",
                    FirstName = "Admin", 
                    LastName = "Admin", 
                    PhoneNumber = "111111111", 
                    Email = "admin@vidRental.com" 
                };

                var adminAdded = await usersManager.CreateAsync(admin, "P@ssword1.");

                if (!adminAdded.Succeeded)
                    throw new Exception("Admin initialization failed");

                var adminRoleAdded = await usersManager.AddToRoleAsync(admin, ApiRoles.Admin);

                if (!adminRoleAdded.Succeeded)
                    throw new Exception("Admin initialization failed");
            }
        } 

    }
}
