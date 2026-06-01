using Microsoft.AspNetCore.Mvc;
using SchoolFeeManagemetSystem.API.Interface;
using static SchoolFeeManagemetSystem.API.DTOs.UserDTOs;

namespace SchoolFeeManagemetSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create( CreateUserDTO dto)
        {
            try
            {
                var result =
                    await _service.CreateAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginDTO dto)
        {
            try
            {
                var result =
                    await _service.LoginAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _service.GetAllAsync());
        }

        // ================= GET BY ID =================

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var users =
                await _service.GetAllAsync();

            var user =
                users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateUserDTO dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest(
                        "Invalid User Id");
                }

                var result = await _service.UpdateAsync(dto);

                if (!result)
                {
                    return NotFound(
                        "User Not Found");
                }

                return Ok(
                    "User Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-account")]
        public async Task<IActionResult> UpdateAccount( UpdateAccountDTO dto)
        {
            try
            {
                var result =
                    await _service.UpdateAccountAsync(dto);

                if (!result)
                {
                    return NotFound();
                }

                return Ok(
                    "Account Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var success =
                await _service.DeleteAsync(id);

            if (!success)
                return NotFound();

            return Ok(
                "Deleted Successfully");
        }
    }
}