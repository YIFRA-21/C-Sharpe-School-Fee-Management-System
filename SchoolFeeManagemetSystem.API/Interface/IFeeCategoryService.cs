using static SchoolFeeManagemetSystem.API.DTOs.FeeCategoryDTOs;

namespace SchoolFeeManagemetSystem.API.Interface
{
    public interface IFeeCategoryService
    {

        Task<List<FeeCategoryDTO>> GetAllAsync();
        Task<FeeCategoryDTO> GetByIdAsync(int id);

        Task<FeeCategoryDTO> CreateAsync(CreateFeeCategoryDTO dto);
        Task<bool> UpdateAsync(UpdateFeeCategoryDTO dto);

        Task<bool> DeleteAsync(int id);
       
    }
}
