﻿namespace SportNewsApp.Services.Users
{
    using System.Security.Claims;

    public interface IUsersService
    {
        string GetUserId(ClaimsPrincipal user);
        string GetUsername(ClaimsPrincipal user);
        bool IfUserExists(string userId);
        bool IfUserAdmin(ClaimsPrincipal user);
    }
}
