using Microsoft.AspNetCore.Identity;
using Server.Models;

namespace Server.Helpers;

public static class DbInitializer
{
    public static async Task SeedRoles(RoleManager<Role> roleManager)
    {
        string[] roleNames = { "Admin", "Seller", "Customer"};

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                });
                Console.WriteLine($"Role '{roleName}' created successfully.");
            }
        }
    }
}