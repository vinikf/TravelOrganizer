using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelOrganizer.Domain.DTOs;

namespace TravelOrganizer.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(UsuarioLogadoFilter))]
    public abstract class BaseController : ControllerBase
    {
        public UsuarioLogadoDTO UsuarioLogado { get; set; }
    }
}
