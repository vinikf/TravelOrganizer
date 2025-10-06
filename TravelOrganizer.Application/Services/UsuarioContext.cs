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
    public class UsuarioContext : IUsuarioContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UsuarioLogadoDTO Usuario
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || !user.Identity.IsAuthenticated)
                    return null;

                return new UsuarioLogadoDTO
                {
                    Id = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"),
                    Nome = user.FindFirstValue(ClaimTypes.Name),
                    Email = user.FindFirstValue(ClaimTypes.Email)
                };
            }
        }
    }
}
