namespace SportNewsApp.Services.Users
{
    using System.Linq;
    using System.Security.Claims;
    public class UsersService : IUsersService
    {
        public string GetUserId(ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        public string GetUsername(ClaimsPrincipal user)
        {
            return user.Identity.Name;
        }

        public bool IfUserExists(string userId)
        {
            return userId != null;
        }
    }
}
