using Microsoft.AspNetCore.Mvc;
using SchoolFeeManagemetSystem.API.Interface;
using static SchoolFeeManagemetSystem.API.DTOs.SystemSettingDTOs;

namespace SchoolFeeManagemetSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemSettingController : ControllerBase
    {
        private readonly ISystemSettingService _service;

        public SystemSettingController(ISystemSettingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateSystemSettingDTO dto)
        {
            var success = await _service.UpdateAsync(dto);
            if (!success) return NotFound();
            return Ok("Updated successfully");
        }
    }
}
