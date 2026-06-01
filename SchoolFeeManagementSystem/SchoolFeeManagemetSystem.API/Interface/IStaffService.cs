using static SchoolFeeManagemetSystem.API.DTOs.StaffDTOs;
namespace SchoolFeeManagemetSystem.API.Interface
{
    public interface IStaffService
    {
        Task<bool> CreateStaffAsync(CreateStaffDto dto);
        Task<bool> UpdateStaffAsync(UpdateStaffDto dto);
        Task<bool> DeleteStaffAsync(int staffId);
        Task<StaffDtos> GetStaffByIdAsync(int staffId);
        Task<List<StaffDtos>> GetAllStaffAsync();
    }
}
