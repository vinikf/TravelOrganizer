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
        public LoggedUserDTO LoggedUser { get; set; }
    }
}
