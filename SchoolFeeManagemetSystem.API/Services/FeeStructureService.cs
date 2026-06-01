using Microsoft.EntityFrameworkCore;
using SchoolFeeManagemetSystem.API.Interface;
using ScoolFeeManagementSystem.Data.Context;
using ScoolFeeManagementSystem.Data.Entities;
using static SchoolFeeManagemetSystem.API.DTOs.FeeStructureDTOs;

namespace SchoolFeeManagemetSystem.API.Services
{
    public class FeeStructureService : IFeeStructureService
    {
        private readonly AppDBContext _context;

        public FeeStructureService(AppDBContext context)
        {
            _context = context;
        }

        // ✅ GET ALL
        public async Task<List<FeeStructureDTO>> GetAllAsync()
        {
            return await _context.FeeStructures
                .AsNoTracking()
                .Include(f => f.FeeCategory)
                .Select(f => new FeeStructureDTO
                {
                    Id = f.Id,
                    Class = f.Class,
                    FeeCategoryId = f.FeeCategoryId,
                    FeeCategoryName = f.FeeCategory != null ? f.FeeCategory.CategoryName : "",
                    Amount = f.Amount,
                    Frequency = f.Frequency,
                    DueDay = f.DueDay,
                    LateFee = f.LateFee,
                    GraceDays = f.GraceDays,
                    Description = f.Description
                }).ToListAsync();
        }

        // ✅ CREATE
        public async Task<FeeStructureDTO> CreateAsync(CreateFeeStructureDTO dto)
        {
            // 🔥 Validate FeeCategory
            var categoryExists = await _context.FeeCategories
                .AnyAsync(x => x.Id == dto.FeeCategoryId);

            if (!categoryExists)
                throw new ArgumentException($"Invalid FeeCategoryId: {dto.FeeCategoryId}");

            var entity = new FeeStructure
            {
                Class = dto.Class,
                FeeCategoryId = dto.FeeCategoryId,
                Amount = dto.Amount,
                Frequency = dto.Frequency,
                DueDay = dto.DueDay,
                LateFee = dto.LateFee,
                GraceDays = dto.GraceDays,
                Description = dto.Description

            };

            _context.FeeStructures.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

        // ✅ GET BY ID
        public async Task<FeeStructureDTO> GetByIdAsync(int id)
        {
            var f = await _context.FeeStructures
                .AsNoTracking()
                .Include(x => x.FeeCategory)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (f == null) return null;

            return new FeeStructureDTO
            {
                Id = f.Id,
                Class = f.Class,
                FeeCategoryId = f.FeeCategoryId,
                FeeCategoryName = f.FeeCategory != null ? f.FeeCategory.CategoryName : "",
                Amount = f.Amount,
                Frequency = f.Frequency,
                DueDay = f.DueDay,
                LateFee = f.LateFee,
                GraceDays = f.GraceDays,
                Description = f.Description
            };
        }

        // ✅ UPDATE (FIXED 🔥)
        public async Task<bool> UpdateAsync(UpdateFeeStructureDTO dto)
        {
            var entity = await _context.FeeStructures.FindAsync(dto.Id);
            if (entity == null) return false;

            // 🔥 IMPORTANT: Validate FK again (this was missing!)
            var categoryExists = await _context.FeeCategories
                .AnyAsync(x => x.Id == dto.FeeCategoryId);

            if (!categoryExists)
                throw new ArgumentException($"Invalid FeeCategoryId: {dto.FeeCategoryId}");

            entity.Class = dto.Class;
            entity.FeeCategoryId = dto.FeeCategoryId;
            entity.Amount = dto.Amount;
            entity.Frequency = dto.Frequency;
            entity.DueDay = dto.DueDay;
            entity.LateFee = dto.LateFee;
            entity.GraceDays = dto.GraceDays;
            entity.Description = dto.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FeeStructures.FindAsync(id);
            if (entity == null) return false;

            _context.FeeStructures.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ GET BY CLASS
        public async Task<List<FeeStructureDTO>> GetByClassAsync(string className)
        {
            return await _context.FeeStructures
                .AsNoTracking()
                .Include(x => x.FeeCategory)
                .Where(x => x.Class == className)
                .Select(x => new FeeStructureDTO
                {
                    Id = x.Id,
                    Class = x.Class,
                    FeeCategoryId = x.FeeCategoryId,
                    FeeCategoryName = x.FeeCategory != null ? x.FeeCategory.CategoryName : "",
                    Amount = x.Amount,
                    Frequency = x.Frequency,
                    DueDay = x.DueDay,
                    LateFee = x.LateFee,
                    GraceDays = x.GraceDays,
                    Description = x.Description
                }).ToListAsync();
        }
    }
}