using static SchoolFeeManagemetSystem.API.DTOs.UserDTOs;

namespace SchoolFeeManagemetSystem.API.Interface
{
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(CreateUserDTO dto);
        Task<UserDTO> LoginAsync(LoginDTO dto);
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAccountAsync(UpdateAccountDTO dto);
        Task<bool> UpdateAccountAsync(UserDTO dto);
        Task<bool> UpdateAsync(UpdateUserDTO dto);
    }
}
