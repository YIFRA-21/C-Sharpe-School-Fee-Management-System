using static SchoolFeeManagemetSystem.API.DTOs.StudentDTOs;

namespace SchoolFeeManagemetSystem.API.Interface
{
    public interface IStudentService
    {
    Task<List<StudentDTO>> GetAllAsync();
    Task<StudentDTO> GetByIdAsync(int id);

    Task<StudentDTO> CreateAsync(CreateStudentDTO dto);
    Task<bool> UpdateAsync(UpdateStudentDTO dto);

    Task<bool> DeleteAsync(int id);
     
    }
}
