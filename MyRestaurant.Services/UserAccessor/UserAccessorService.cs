﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MyRestaurant.Services
{
    public class UserAccessorService : IUserAccessorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessorService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private static Guid GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            var id = claimsPrincipal.FindFirstValue("id");

            if (id == null)
                return Guid.Empty;

            return new Guid(id);
        }

        public CurrentUser? GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext == null) return null;

            var claimPrincipal = _httpContextAccessor.HttpContext.User;

            if (claimPrincipal == null) return null;

            return new CurrentUser
            {
                UserId = GetUserId(claimPrincipal),
                Email = claimPrincipal.FindFirstValue(ClaimTypes.Email),
                FirstName = claimPrincipal.FindFirstValue("firstName"),
                LastName = claimPrincipal.FindFirstValue("lastName"),
                Roles = claimPrincipal.FindAll(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList()
            };
        }
    }
}
