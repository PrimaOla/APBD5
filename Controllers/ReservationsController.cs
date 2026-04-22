using Microsoft.AspNetCore.Mvc;
using APBD5.Models;
using APBD5.Data;

namespace APBD5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private static List<Reservation> Reservations => InMemoryStore.Reservations;
        private static List<Room> Rooms => InMemoryStore.Rooms;

        // GET: api/reservations
        // GET: api/reservations?date=2026-05-10&status=confirmed&roomId=2
        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] DateOnly? date,
            [FromQuery] string? status,
            [FromQuery] int? roomId)
        {
            IEnumerable<Reservation> result = Reservations;

            if (date.HasValue)
                result = result.Where(r => r.Date == date.Value);

            if (!string.IsNullOrWhiteSpace(status))
                result = result.Where(r => r.Status == status);

            if (roomId.HasValue)
                result = result.Where(r => r.RoomId == roomId.Value);

            return Ok(result.ToList());
        }

        // GET: api/reservations/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reservation = Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        // POST: api/reservations
        [HttpPost]
        public IActionResult Create([FromBody] Reservation reservation)
        {
            var businessError = ValidateBusiness(reservation, ignoreReservationId: null);
            if (businessError != null) return businessError;

            reservation.Id = Reservations.Count == 0 ? 1 : Reservations.Max(r => r.Id) + 1;
            Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

        // PUT: api/reservations/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Reservation updated)
        {
            var existing = Reservations.FirstOrDefault(r => r.Id == id);
            if (existing == null) return NotFound();

            var businessError = ValidateBusiness(updated, ignoreReservationId: id);
            if (businessError != null) return businessError;

            existing.RoomId = updated.RoomId;
            existing.OrganizerName = updated.OrganizerName;
            existing.Topic = updated.Topic;
            existing.Date = updated.Date;
            existing.StartTime = updated.StartTime;
            existing.EndTime = updated.EndTime;
            existing.Status = updated.Status;

            return Ok(existing);
        }

        // DELETE: api/reservations/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservation = Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return NotFound();

            Reservations.Remove(reservation);
            return NoContent();
        }

        private IActionResult? ValidateBusiness(Reservation reservation, int? ignoreReservationId)
        {
            var room = Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
            if (room == null)
                return NotFound(new { message = $"Room with id {reservation.RoomId} does not exist." });

            if (!room.IsActive)
                return Conflict(new { message = $"Room {room.Id} is not active." });

            if (HasConflict(reservation, ignoreReservationId))
                return Conflict(new { message = "Reservation time overlaps with an existing reservation for this room." });

            return null;
        }

        private bool HasConflict(Reservation candidate, int? ignoreReservationId)
        {
            return Reservations.Any(existing =>
                existing.Id != ignoreReservationId &&
                existing.RoomId == candidate.RoomId &&
                existing.Date == candidate.Date &&
                existing.StartTime < candidate.EndTime &&
                candidate.StartTime < existing.EndTime);
        }
    }
}
