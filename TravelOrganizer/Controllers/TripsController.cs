using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelOrganizer.Application;
using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Domain.DTOs;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Api.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : BaseController
    {
        private readonly ITripApplication _tripApplication;

        public TripsController(ITripApplication tripApplication)
        {
            _tripApplication = tripApplication;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] TripDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid request payload.");

            try
            {
                await _tripApplication.Create(dto);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the trip.");
            }
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<Trip> trips = await _tripApplication.List();
                return Ok(trips);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving trips.");
            }
        }
    }
}
