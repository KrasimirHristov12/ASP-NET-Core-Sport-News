namespace SportNewsApp.Services.Users
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        string GetUserId(ClaimsPrincipal user);
        string GetUsername(ClaimsPrincipal user);
        Task<string> GetUsernameByUserId(string userId);
        bool IfUserExists(string userId);
        bool IfUserAdmin(ClaimsPrincipal user);
    }
}
