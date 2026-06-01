using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;

namespace SchoolFeeManagemetSystem.API.Interface
{
    public interface IPaymentService
    {
        Task<List<PaymentDTO>> GetAllAsync();
        Task<PaymentDTO> GetByIdAsync(int id);

        Task<PaymentDTO> CreateAsync(CreatePaymentDTO dto);
        Task<bool> UpdateAsync(UpdatePaymentDTO dto);

        Task<bool> DeleteAsync(int id);

        Task<List<PaymentDTO>> GetByStudentAsync(int studentId);
        Task<decimal> GetTotalPaidAsync();
    }
}
