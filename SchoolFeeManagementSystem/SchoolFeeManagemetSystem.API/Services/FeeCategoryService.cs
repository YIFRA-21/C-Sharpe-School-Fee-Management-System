using SchoolFeeManagemetSystem.API.Interface;
using ScoolFeeManagementSystem.Data.Context;
using ScoolFeeManagementSystem.Data.Entities;
using static SchoolFeeManagemetSystem.API.DTOs.FeeCategoryDTOs;
using Microsoft.EntityFrameworkCore;

namespace SchoolFeeManagemetSystem.API.Services
{
    public class FeeCategoryService : IFeeCategoryService
    {
        private readonly AppDBContext _context;

        public FeeCategoryService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<FeeCategoryDTO>> GetAllAsync()
        {
            return await _context.FeeCategories
                .Select(f => new FeeCategoryDTO
                {
                    Id = f.Id,
                    CategoryCode = f.CategoryCode,
                    CategoryName = f.CategoryName,
                    Description = f.Description,
                    Frequency = f.Frequency,
                    IsActive = f.IsActive
                }).ToListAsync();
        }

        public async Task<FeeCategoryDTO> CreateAsync(CreateFeeCategoryDTO dto)
        {
            var entity = new FeeCategory
            {
                CategoryCode = dto.CategoryCode,
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                Frequency = dto.Frequency,
                IsActive = dto.IsActive
            };

            _context.FeeCategories.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

        public async Task<FeeCategoryDTO> GetByIdAsync(int id)
        {
            var f = await _context.FeeCategories.FindAsync(id);
            if (f == null) return null;

            return new FeeCategoryDTO
            {
                Id = f.Id,
                CategoryCode = f.CategoryCode,
                CategoryName = f.CategoryName,
                Description = f.Description,
                Frequency = f.Frequency,
                IsActive = f.IsActive
            };
        }

        public async Task<bool> UpdateAsync(UpdateFeeCategoryDTO dto)
        {
            var entity = await _context.FeeCategories.FindAsync(dto.Id);
            if (entity == null) return false;

            entity.CategoryCode = dto.CategoryCode;
            entity.CategoryName = dto.CategoryName;
            entity.Description = dto.Description;
            entity.Frequency = dto.Frequency;
            entity.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FeeCategories.FindAsync(id);
            if (entity == null) return false;

            _context.FeeCategories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
