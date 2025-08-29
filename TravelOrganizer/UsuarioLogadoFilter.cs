using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using TravelOrganizer.Api.Controllers;
using TravelOrganizer.Domain.DTOs;

namespace TravelOrganizer.Api
{
    public class UsuarioLogadoFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is BaseController baseController)
            {
                var user = context.HttpContext.User;
                if (user?.Identity?.IsAuthenticated == true)
                {
                    baseController.UsuarioLogado = new UsuarioLogadoDTO
                    {
                        Id = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"),
                        Nome = user.FindFirstValue(ClaimTypes.Name),
                        Email = user.FindFirstValue(ClaimTypes.Email)
                    };
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
