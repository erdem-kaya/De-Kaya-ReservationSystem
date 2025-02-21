using Business.Interfaces;
using Business.Models.CoolingRoom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/coolingroom")]
    [ApiController]
    public class CoolingRoomController(ICoolingRoomService coolingRoomService) : ControllerBase
    {
        private readonly ICoolingRoomService _coolingRoomService = coolingRoomService;

        [HttpPost]
        public async Task<IActionResult> CreateCoolingRoomAsync(CoolingRoomRegistrationForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var createdCoolingRoom = await _coolingRoomService.CreateAsync(form);
            var result = createdCoolingRoom != null ? Ok(createdCoolingRoom) : Problem("Cooling room could not be created.");
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoolingRoomsAsync()
        {
            var allCoolingRooms = await _coolingRoomService.GetAllAsync();
            if (allCoolingRooms != null)
                return Ok(allCoolingRooms);
            return NotFound("There are no cooling rooms to view.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoolingRoomByIdAsync(int id)
        {
            var coolingRoom = await _coolingRoomService.GetByIdAsync(id);
            if (coolingRoom != null)
                return Ok(coolingRoom);
            return NotFound($"Cooling room with id {id} not found.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoolingRoomAsync(int id, CoolingRoomUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                var coolingRoom = await _coolingRoomService.GetByIdAsync(id);
                if (coolingRoom == null)
                    return NotFound($"Cooling room with id {id} not found.");
                var updatedCoolingRoom = await _coolingRoomService.UpdateAsync(form);
                return updatedCoolingRoom != null ? Ok(updatedCoolingRoom) : Problem("Cooling room could not be updated.");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoolingRoomAsync(int id)
        {
            var deleted = await _coolingRoomService.DeleteAsync(id);
            return deleted ? Ok() : NotFound($"Cooling room with id {id} not found.");
        }

        [HttpPut("updateallprices")]
        public async Task<IActionResult> UpdateAllCoolingRoomPricesAsync(decimal newPrice)
        {
            var updated = await _coolingRoomService.UpdateAllCoolingRoomPricesAsync(newPrice);
            return updated ? Ok() : Problem("Prices could not be updated.");
        }
    }
}
