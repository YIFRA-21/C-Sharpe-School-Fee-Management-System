using static SchoolFeeManagemetSystem.API.DTOs.FeeStructureDTOs;

namespace SchoolFeeManagemetSystem.API.Interface
{
    public interface IFeeStructureService
    {
        Task<List<FeeStructureDTO>> GetAllAsync();
        Task<FeeStructureDTO> GetByIdAsync(int id);

        Task<FeeStructureDTO> CreateAsync(CreateFeeStructureDTO dto);
        Task<bool> UpdateAsync(UpdateFeeStructureDTO dto);

        Task<bool> DeleteAsync(int id);

        Task<List<FeeStructureDTO>> GetByClassAsync(string className);
    }
}
