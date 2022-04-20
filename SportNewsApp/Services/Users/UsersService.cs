namespace SportNewsApp.Services.Users
{
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UsersService : IUsersService
    {
        private readonly UserManager<IdentityUser> userManager;

        public UsersService(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public string GetUserId(ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        public string GetUsername(ClaimsPrincipal user)
        {
            return user.Identity.Name;
        }

        public async Task<string> GetUsernameByUserId(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user.UserName;
        }

        public bool IfUserAdmin(ClaimsPrincipal user)
        {
            return user.IsInRole("Admin");
        }

        public bool IfUserExists(string userId)
        {
            return userId != null;
        }
    }
}
