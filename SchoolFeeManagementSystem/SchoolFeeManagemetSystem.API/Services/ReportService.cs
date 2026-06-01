using Microsoft.EntityFrameworkCore;
using SchoolFeeManagemetSystem.API.Interface;
using ScoolFeeManagementSystem.Data.Context;
using static SchoolFeeManagemetSystem.API.DTOs.ReportDTOs;
using ReportDTO = SchoolFeeManagemetSystem.API.Interface.ReportDTO;

public class ReportService : IReportService
{
    private readonly AppDBContext _context;

    public ReportService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<ReportDTO>> GetReportsAsync(
        DateTime from,
        DateTime to,
        string className,
        string category)
    {
        var query = _context.Payments
            .Include(p => p.Student)
            .Include(p => p.FeeStructure)
                .ThenInclude(f => f.FeeCategory)
            .Where(p => p.PaymentDate >= from && p.PaymentDate <= to)
            .AsQueryable();

        if (!string.IsNullOrEmpty(className))
            query = query.Where(p => p.Class == className);

        if (!string.IsNullOrEmpty(category))
            query = query.Where(p => p.FeeStructure.FeeCategory.CategoryName == category);

        return await query.Select(p => new ReportDTO
        {
            ReceiptNo = p.ReceiptNo,
            StudentName = p.Student.FullName,
            Class = p.Class,
            FeeCategory = p.FeeStructure.FeeCategory.CategoryName,
            Amount = p.PaidAmount,
            PaymentDate = p.PaymentDate,
            PaymentMethod = p.PaymentMethod,
            ReferenceNo = p.ReferenceNo
        }).ToListAsync();
    }

    public async Task<List<ReportDTO>> GetReportsAsync(ReportFilterDTO filter)
    {
        return await GetReportsAsync(
            filter?.FromDate ?? DateTime.MinValue,
            filter?.ToDate ?? DateTime.MaxValue,
            filter?.Class,
            filter?.FeeCategory
        );
    }

    public async Task<decimal> GetTotalCollectionAsync(DateTime from, DateTime to)
    {
        return await _context.Payments
            .Where(p => p.PaymentDate >= from && p.PaymentDate <= to)
            .SumAsync(p => p.PaidAmount);
    }

    public async Task<decimal> GetTotalOutstandingAsync()
    {
        return await _context.Payments.SumAsync(p => p.Balance);
    }

    public async Task<int> GetTotalStudentsAsync()
    {
        return await _context.Students.CountAsync();
    }
}