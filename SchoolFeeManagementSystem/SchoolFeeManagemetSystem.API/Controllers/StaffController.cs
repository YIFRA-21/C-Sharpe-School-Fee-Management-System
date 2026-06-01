using Microsoft.AspNetCore.Mvc;
using SchoolFeeManagemetSystem.API.DTOs;
using SchoolFeeManagemetSystem.API.Interface;

namespace SchoolFeeManagemetSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var result = await _staffService.GetAllStaffAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var result = await _staffService.GetStaffByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

    
        [HttpPost]
        public async Task<IActionResult> CreateStaff( [FromBody] StaffDTOs.CreateStaffDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _staffService.CreateStaffAsync(dto);

            if (result == null)
                return BadRequest("Failed to create staff");

            return Ok("Staff Created Successfully");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStaff(
            [FromBody] StaffDTOs.UpdateStaffDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _staffService.UpdateStaffAsync(dto);

            if (!result)
                return NotFound("Staff Not Found");

            return Ok("Staff Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var result = await _staffService.DeleteStaffAsync(id);

            if (!result)
                return NotFound("Staff Not Found");

            return Ok("Staff Deleted Successfully");
        }
    }
}
