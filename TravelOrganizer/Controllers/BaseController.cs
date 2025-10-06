using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelOrganizer.Domain.DTOs;

namespace TravelOrganizer.Api.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public UsuarioLogadoDTO UsuarioLogado { get; set; }
    }
}
