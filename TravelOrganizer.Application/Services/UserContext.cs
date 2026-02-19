using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Domain.DTOs;

namespace TravelOrganizer.Application.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public LoggedUserDTO User
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user != null && user.Identity.IsAuthenticated)
                    return new LoggedUserDTO
                    {
                        Id = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"),
                        Name = user.FindFirstValue(ClaimTypes.Name),
                        Email = user.FindFirstValue(ClaimTypes.Email)
                    };

                return null;
            }
        }
    }
}
