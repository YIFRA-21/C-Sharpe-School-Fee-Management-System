using Microsoft.AspNetCore.Mvc;
using SchoolFeeManagemetSystem.API.Interface;
using static SchoolFeeManagemetSystem.API.DTOs.FeeCategoryDTOs;

namespace SchoolFeeManagemetSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeeCategoryController : ControllerBase
    {
        private readonly IFeeCategoryService _service;

        public FeeCategoryController(IFeeCategoryService service)
        {
            _service = service;
        }

        // ✅ GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // ✅ CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeeCategoryDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        // ✅ UPDATE (FIXED 🔥)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFeeCategoryDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var exists = await _service.GetByIdAsync(id);
            if (exists == null)
                return NotFound();

          
            await _service.UpdateAsync(dto);

            return Ok("Updated successfully");
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
                return NotFound();

            return Ok("Deleted successfully");
        }
    }
}