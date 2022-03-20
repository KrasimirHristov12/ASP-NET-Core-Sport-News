namespace SportNewsApp.Services.Users
{
    using System.Linq;
    using System.Security.Claims;
    public class UsersService : IUsersService
    {
        public string GetUserId(ClaimsPrincipal user)
            => user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        public string GetUsername(ClaimsPrincipal user)
        {
            return user.Identity.Name;
        }
    }
}
