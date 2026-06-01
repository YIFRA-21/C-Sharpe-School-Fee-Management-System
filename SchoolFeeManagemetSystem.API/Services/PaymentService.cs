using Microsoft.EntityFrameworkCore;
using SchoolFeeManagemetSystem.API.Interface;
using ScoolFeeManagementSystem.Data.Context;
using ScoolFeeManagementSystem.Data.Entities;
using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;

namespace SchoolFeeManagemetSystem.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDBContext _context;

        public PaymentService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentDTO>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Student)
                .Include(p => p.FeeStructure).ThenInclude(f => f.FeeCategory)
                .Select(p => new PaymentDTO
                {
                    Id = p.Id,
                    ReceiptNo = p.ReceiptNo,
                    StudentName = p.Student.FullName,
                    FeeCategory = p.FeeStructure.FeeCategory.CategoryName,
                    Class = p.Class,
                    TotalFee = p.TotalFee,
                    PaidAmount = p.PaidAmount,
                    Balance = p.Balance,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    ReferenceNo = p.ReferenceNo
                }).ToListAsync();
        }

        public async Task<PaymentDTO> CreateAsync(CreatePaymentDTO dto)
        {
            // ✅ CHECK FeeStructure exists
            var feeStructure = await _context.FeeStructures
                .FirstOrDefaultAsync(f => f.Id == dto.FeeStructureId);

            if (feeStructure == null)
                throw new Exception($"FeeStructureId {dto.FeeStructureId} not found in database.");

            // ✅ CHECK Student exists
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == dto.StudentId);

            if (student == null)
                throw new Exception("Invalid Student selected!");

            var entity = new Payment
            {
                ReceiptNo = dto.ReceiptNo,
                StudentId = dto.StudentId,
                FeeStructureId = dto.FeeStructureId,
                Class = dto.Class,
                TotalFee = dto.TotalFee,
                PaidAmount = dto.PaidAmount,
                Balance = dto.TotalFee - dto.PaidAmount,
                PaymentDate = dto.PaymentDate,
                PaymentMethod = dto.PaymentMethod,
                ReferenceNo = dto.ReferenceNo,
                Remarks = dto.Remarks
            };

            _context.Payments.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

      public async Task<PaymentDTO> GetByIdAsync(int id)
{
    return await _context.Payments
        .Where(p => p.Id == id)
        .Select(p => new PaymentDTO
        {
            Id = p.Id,
            ReceiptNo = p.ReceiptNo,
            StudentName = p.Student.FullName,
            FeeCategory = p.FeeStructure.FeeCategory.CategoryName,
            Class = p.Class,
            TotalFee = p.TotalFee,
            PaidAmount = p.PaidAmount,
            Balance = p.Balance,
            PaymentDate = p.PaymentDate,
            PaymentMethod = p.PaymentMethod,
            ReferenceNo = p.ReferenceNo
        }).FirstOrDefaultAsync();
}

        public async Task<bool> UpdateAsync(UpdatePaymentDTO dto)
        {
            var entity = await _context.Payments.FindAsync(dto.Id);
            if (entity == null) return false;

            entity.PaidAmount = dto.PaidAmount;
            entity.Balance = dto.Balance;
            entity.PaymentMethod = dto.PaymentMethod;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Payments.FindAsync(id);
            if (entity == null) return false;

            _context.Payments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PaymentDTO>> GetByStudentAsync(int studentId)
        {
            return await _context.Payments
                .Where(p => p.StudentId == studentId)
                .Include(p => p.Student)
                .Include(p => p.FeeStructure).ThenInclude(f => f.FeeCategory)
                .Select(p => new PaymentDTO
                {
                    Id = p.Id,
                    ReceiptNo = p.ReceiptNo,
                    StudentName = p.Student.FullName,
                    FeeCategory = p.FeeStructure.FeeCategory.CategoryName,
                    Class = p.Class,
                    TotalFee = p.TotalFee,
                    PaidAmount = p.PaidAmount,
                    Balance = p.Balance,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    ReferenceNo = p.ReferenceNo
                }).ToListAsync();
        }
        public async Task<decimal> GetTotalPaidAsync()
        {
            return await _context.Payments.SumAsync(x => x.PaidAmount);
        }
    }
}
