namespace SportNewsApp
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SportNewsApp.Data;
    using SportNewsApp.Services.Articles;
    using SportNewsApp.Services.Authors;
    using SportNewsApp.Services.Categories;
    using SportNewsApp.Services.Users;
    using System.Threading.Tasks;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddTransient<IAuthorsService, AuthorsService>();
            services.AddTransient<IArticlesService, ArticlesService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IUsersService, UsersService>();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                var role = new IdentityRole
                {
                    Name = "Author"
                };
                var doesRoleExist = await roleManager.RoleExistsAsync(role.Name);
                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(role);
                }
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };
                var doesRoleExist = await roleManager.RoleExistsAsync(role.Name);
                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(role);
                }
            }).GetAwaiter().GetResult();
        }
    }
}
