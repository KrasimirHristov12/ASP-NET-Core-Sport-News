namespace SportNewsApp.Services.Users
{
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Security.Claims;
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
