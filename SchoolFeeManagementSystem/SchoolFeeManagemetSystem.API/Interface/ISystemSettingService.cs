using SchoolFeeManagemetSystem.API.DTOs;
using static SchoolFeeManagemetSystem.API.DTOs.SystemSettingDTOs;

namespace SchoolFeeManagemetSystem.API.Interface
{
    public interface ISystemSettingService
    {
        Task<SystemSettingDTOs> GetAsync();

        Task<bool> UpdateAsync(UpdateSystemSettingDTO dto);
    }
}
