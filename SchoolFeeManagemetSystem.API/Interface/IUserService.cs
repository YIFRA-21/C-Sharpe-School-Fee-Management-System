using static SchoolFeeManagemetSystem.API.DTOs.UserDTOs;

namespace SchoolFeeManagemetSystem.API.Interface
{
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(CreateUserDTO dto);
        Task<UserDTO> LoginAsync(LoginDTO dto);
        Task<List<UserDTO>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
