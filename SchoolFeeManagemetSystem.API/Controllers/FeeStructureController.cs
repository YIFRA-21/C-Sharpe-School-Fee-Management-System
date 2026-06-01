using Microsoft.AspNetCore.Mvc;
using SchoolFeeManagemetSystem.API.Interface;
using static SchoolFeeManagemetSystem.API.DTOs.FeeStructureDTOs;

namespace SchoolFeeManagemetSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeeStructureController : ControllerBase
    {
        private readonly IFeeStructureService _service;

        public FeeStructureController(IFeeStructureService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFeeStructureDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        // ✅ FIXED UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateFeeStructureDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var success = await _service.UpdateAsync(dto);

            if (!success)
                return NotFound();

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