using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportNewsApp.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CreateAppRolesMiddleware
    {
        private readonly RequestDelegate _next;

        public CreateAppRolesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            await CreateRole("User", roleManager);
            await CreateRole("Author", roleManager);
            await CreateRole("Admin", roleManager);
            await CreateAdminAccount("admin@admin.com", "admin123", userManager, signInManager);
            await _next(httpContext);
        }
        private async Task CreateRole(string roleName, RoleManager<IdentityRole> roleManager)
        {
            var role = new IdentityRole
            {
                Name = roleName
            };
            var doesRoleExist = await roleManager.RoleExistsAsync(roleName);
            if (!doesRoleExist)
            {
                await roleManager.CreateAsync(role);
            }
        }
        private async Task CreateAdminAccount(string email, string password, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            var adminUser = new IdentityUser()
            {
                Email = email,
                UserName = email,

            };

            var result = await userManager.CreateAsync(adminUser, password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(adminUser, false);
                var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CreateAppRolesMiddlewareExtensions
    {
        public static IApplicationBuilder UseCreateAppRolesMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CreateAppRolesMiddleware>();
        }
    }

}

