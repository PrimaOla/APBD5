using Microsoft.AspNetCore.Mvc;
using APBD5.Models;
using APBD5.Data;

namespace APBD5.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private static List<Room> Rooms => InMemoryStore.Rooms;
    
    // GET: api/rooms
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Rooms);
    }

    // GET: api/rooms/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var room = Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    // GET: api/rooms/buinding/{buildlingCode}
    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuilding(string buildingCode)
    {
        var result = Rooms.Where( r => r.BuildingCode == buildingCode).ToList();
        return Ok(result);
    }

    // GET: api/rooms??minCapacity=20&hasProjector=true&activeOnly=true
    [HttpGet("filter")]
    public IActionResult Filter([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
    {
        var result = Rooms.AsQueryable();

        if (minCapacity.HasValue)
            result = result.Where(r => r.Capacity >= minCapacity);

        if (hasProjector.HasValue)
            result = result.Where(r => r.HasProjector == hasProjector);

        if (activeOnly == true)
            result = result.Where(r => r.IsActive);

        return Ok(result);
    }

    // POST: api/rooms
    [HttpPost]
    public IActionResult Create(Room room)
    {
        room.Id = Rooms.Max(r => r.Id) + 1;
        Rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id}, room);
    }

    // PUT: api/rooms/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, Room updateRoom)
    {
        var room = Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound(); 

        room.Name = updateRoom.Name;
        room.BuildingCode = updateRoom.BuildingCode;
        room.Floor = updateRoom.Floor;
        room.Capacity = updateRoom.Capacity;
        room.HasProjector = updateRoom.HasProjector;
        room.IsActive = updateRoom.IsActive;

        return Ok(room);
    }


    // DELETE: api/rooms/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        Rooms.Remove(room);

        return NoContent();
    }
    }
}