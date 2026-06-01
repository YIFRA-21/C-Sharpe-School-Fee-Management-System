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

        // ================= GET ALL =================

        public async Task<List<PaymentDTO>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Student)
                .Include(p => p.FeeStructure)
                .ThenInclude(f => f.FeeCategory)

                .Select(p => new PaymentDTO
                {
                    Id = p.Id,

                    ReceiptNo = p.ReceiptNo,
                    StudentId = p.StudentId,
                    FeeStructureId = p.FeeStructureId,
                    StudentName = p.Student.FullName,
                    FeeCategory = p.FeeStructure.FeeCategory.CategoryName,

                    Class = p.Class,

                    TotalFee = p.TotalFee,
                    PaidAmount = p.PaidAmount,
                    Balance = p.Balance,

                    PaymentDate = p.PaymentDate,

                    PaymentMethod = p.PaymentMethod,

                    ReferenceNo = p.ReferenceNo,

                    Remarks = p.Remarks
                })

                .ToListAsync();
        }

        public async Task<PaymentDTO> CreateAsync(CreatePaymentDTO dto)
        {
            // CHECK STUDENT
            var student = await _context.Students
                .FirstOrDefaultAsync(x => x.Id == dto.StudentId);

            if (student == null)
                throw new Exception("Student not found");
            var feeStructure = await _context.FeeStructures
                .FirstOrDefaultAsync(x => x.Id == dto.FeeStructureId);

            if (feeStructure == null)
                throw new Exception("Fee Structure not found");

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

        // ================= GET BY ID =================

        public async Task<PaymentDTO> GetByIdAsync(int id)
        {
            return await _context.Payments

                .Include(p => p.Student)

                .Include(p => p.FeeStructure)
                .ThenInclude(f => f.FeeCategory)

                .Where(p => p.Id == id)

                .Select(p => new PaymentDTO
                {
                    Id = p.Id,

                    ReceiptNo = p.ReceiptNo,
                    StudentId = p.StudentId,
                    FeeStructureId = p.FeeStructureId,

                    StudentName = p.Student.FullName,

                    FeeCategory =
                        p.FeeStructure.FeeCategory.CategoryName,

                    Class = p.Class,

                    TotalFee = p.TotalFee,

                    PaidAmount = p.PaidAmount,

                    Balance = p.Balance,

                    PaymentDate = p.PaymentDate,

                    PaymentMethod = p.PaymentMethod,

                    ReferenceNo = p.ReferenceNo,

                    Remarks = p.Remarks
                })

                .FirstOrDefaultAsync();
        }

        // ================= UPDATE =================

        public async Task<bool> UpdateAsync(UpdatePaymentDTO dto)
        {
            var entity = await _context.Payments
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (entity == null)
                return false;

            entity.ReceiptNo = dto.ReceiptNo;

            entity.StudentId = dto.StudentId;

            entity.FeeStructureId = dto.FeeStructureId;

            entity.Class = dto.Class;

            entity.TotalFee = dto.TotalFee;

            entity.PaidAmount = dto.PaidAmount;

            entity.Balance = dto.TotalFee - dto.PaidAmount;

            entity.PaymentDate = dto.PaymentDate;

            entity.PaymentMethod = dto.PaymentMethod;

            entity.ReferenceNo = dto.ReferenceNo;

            entity.Remarks = dto.Remarks;

            await _context.SaveChangesAsync();

            return true;
        }

        // ================= DELETE =================

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Payments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return false;

            _context.Payments.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        // ================= GET BY STUDENT =================

        public async Task<List<PaymentDTO>> GetByStudentAsync(int studentId)
        {
            return await _context.Payments

                .Where(p => p.StudentId == studentId)

                .Include(p => p.Student)

                .Include(p => p.FeeStructure)
                .ThenInclude(f => f.FeeCategory)

                .Select(p => new PaymentDTO
                {
                    Id = p.Id,

                    ReceiptNo = p.ReceiptNo,

                    // ================= FIXED =================
                    StudentId = p.StudentId,
                    FeeStructureId = p.FeeStructureId,

                    StudentName = p.Student.FullName,

                    FeeCategory =
                        p.FeeStructure.FeeCategory.CategoryName,

                    Class = p.Class,

                    TotalFee = p.TotalFee,

                    PaidAmount = p.PaidAmount,

                    Balance = p.Balance,

                    PaymentDate = p.PaymentDate,

                    PaymentMethod = p.PaymentMethod,

                    ReferenceNo = p.ReferenceNo,

                    Remarks = p.Remarks
                })

                .ToListAsync();
        }

        // ================= TOTAL PAID =================

        public async Task<decimal> GetTotalPaidAsync()
        {
            return await _context.Payments
                .SumAsync(x => x.PaidAmount);
        }
    }
}