using Microsoft.AspNetCore.Mvc;
using SchoolFeeManagemetSystem.API.Interface;
using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;

namespace SchoolFeeManagemetSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return data == null ? NotFound() : Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentDTO dto)
            => Ok(await _service.CreateAsync(dto));

      
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePaymentDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var success = await _service.UpdateAsync(dto);
            return success ? Ok("Updated successfully") : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok("Deleted successfully") : NotFound();
        }
    }
}
