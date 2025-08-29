using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelOrganizer.Application;
using TravelOrganizer.Domain.DTOs;

namespace TravelOrganizer.Api.Controllers
{
    [Route("api/[controller]")]
    public class ViagensController : BaseController
    {

        public ViagensController()
        {
        }

        [HttpPost("Criar")]
        public async Task<IActionResult> CriarViagem([FromBody] NovaViagemDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid request payload.");

            try
            {
                await new ViagemApplication(UsuarioLogado).CriarViagem(dto);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the trip.");
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarViagens()
        {
            try
            {
                var viagens = await new ViagemApplication(UsuarioLogado).ListarViagens();
                return Ok(viagens);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving trips.");
            }
        }
    }
}
