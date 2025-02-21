using Business.Interfaces;
using Business.Models.CoolingRoomInventory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/cooling-inventory")]
    [ApiController]
    public class CoolingRoomInventoryController(ICoolingRoomInventoryService coolingRoomInventoryService) : ControllerBase
    {
        private readonly ICoolingRoomInventoryService _coolingRoomInventoryService = coolingRoomInventoryService;

        [HttpPost]
        public async Task<IActionResult> CreateCoolingRoomInventoryAsync(CoolingRoomInventoryRegistrationForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var createdCoolingRoomInventory = await _coolingRoomInventoryService.CreateAsync(form);
            var result = createdCoolingRoomInventory != null ? Ok(createdCoolingRoomInventory) : Problem("Cooling room inventory could not be created.");
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoolingRoomInventoriesAsync()
        {
            var allCoolingRoomInventories = await _coolingRoomInventoryService.GetAllAsync();
            if (allCoolingRoomInventories != null)
                return Ok(allCoolingRoomInventories);
            return NotFound("There are no cooling room inventories to view.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoolingRoomInventoryByIdAsync(int id)
        {
            var coolingRoomInventory = await _coolingRoomInventoryService.GetByIdAsync(id);
            if (coolingRoomInventory != null)
                return Ok(coolingRoomInventory);
            return NotFound($"Cooling room inventory with id {id} not found.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoolingRoomInventoryAsync(int id, CoolingRoomInventoryUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                var coolingRoomInventory = await _coolingRoomInventoryService.GetByIdAsync(id);
                if (coolingRoomInventory == null)
                    return NotFound($"Cooling room inventory with id {id} not found.");
                var updatedCoolingRoomInventory = await _coolingRoomInventoryService.UpdateAsync(form);
                return updatedCoolingRoomInventory != null ? Ok(updatedCoolingRoomInventory) : Problem("Cooling room inventory could not be updated.");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoolingRoomInventoryAsync(int id)
        {
            var deleted = await _coolingRoomInventoryService.DeleteAsync(id);
            return deleted ? Ok() : NotFound($"Cooling room inventory with id {id} not found.");
        }
    }
}
