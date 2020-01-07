using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.Models;
using Api.Repository;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsRepository _repository;

        public ReservationsController(HotelAvatarContext context)
        {
            _repository = new ReservationsRepository(context);
        }
        
        [HttpGet]
        public IEnumerable<Reservations> GetReservations()
        {
            return _repository.GetReservations();
        }
        
        [HttpGet("{id}")]
        public Reservations GetReservations([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.Exception("Invalid model");
            }
            return _repository.GetReservations(id);
        }
        
        [HttpGet("last")]
        public int GetLastReservations()
        {
            return _repository.GetLastReservations();
        }
        
        [HttpPost]
        public async Task<IActionResult> PostReservations([FromBody] Reservations reservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.PostReservations(reservations);
            return CreatedAtAction("GetReservations", new { id = reservations.ID }, reservations);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservations([FromRoute] int id, [FromBody] Reservations reservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservations.ID)
            {
                return BadRequest();
            }

            await _repository.PutReservations(id, reservations);

           return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservations([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _repository.DeleteReservations(id));
        }

        private bool ReservationsExists(int id)
        {
            return _repository.ReservationsExists(id);
        }
    }
}