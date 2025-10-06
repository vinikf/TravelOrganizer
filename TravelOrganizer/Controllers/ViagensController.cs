using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelOrganizer.Application;
using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Domain.DTOs;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Api.Controllers
{
    [Route("api/[controller]")]
    public class ViagensController : BaseController
    {
        private readonly IViagemApplication _viagemApplication;

        public ViagensController(IViagemApplication viagemApplication)
        {
            _viagemApplication = viagemApplication;
        }

        [HttpPost("Criar")]
        public async Task<IActionResult> CriarViagem([FromBody] NovaViagemDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid request payload.");

            try
            {
                await _viagemApplication.CriarViagem(dto);
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
                List<Viagem> viagens = await _viagemApplication.ListarViagens();
                return Ok(viagens);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving trips.");
            }
        }
    }
}
