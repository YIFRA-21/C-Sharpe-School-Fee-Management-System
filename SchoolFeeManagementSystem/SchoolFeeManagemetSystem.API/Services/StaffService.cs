using SchoolFeeManagemetSystem.API.Interface;
using Microsoft.EntityFrameworkCore;
using ScoolFeeManagementSystem.Data.Context;
using ScoolFeeManagementSystem.Data.Entities;
using static SchoolFeeManagemetSystem.API.DTOs.StaffDTOs;


namespace SchoolFeeManagemetSystem.API.Services
{
  

        public class StaffService : IStaffService
        {
            private readonly AppDBContext _context;

            public StaffService(AppDBContext context)
            {
                _context = context;
            }

        public async Task<List<StaffDtos>> GetAllStaffAsync()
        {
            return await _context.Staff
                 .Select(s => new StaffDtos
                 {
                     StaffId = s.StaffId,
                     FullName = s.FullName,
                     Position = s.Position,
                     Department = s.Department,
                     Email = s.Email,
                     Phone = s.Phone,
                     Address = s.Address,
                     Salary = s.Salary
                 })
                 .ToListAsync();
        }


        public async Task<StaffDtos> GetStaffByIdAsync(int staffId)
        {

            var s = await _context.Staff.FindAsync(staffId);

            if (s == null)
                return null;

            return new StaffDtos
            {
                StaffId = s.StaffId,
                FullName = s.FullName,
                Position = s.Position,
                Department = s.Department,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                Salary = s.Salary
            };
        }
        public async Task<bool> CreateStaffAsync(CreateStaffDto dto)
        {
            var entity = new Staff
            {
                FullName = dto.FullName,
                Position = dto.Position,
                Department = dto.Department,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                Salary = dto.Salary
            };

            await _context.Staff.AddAsync(entity);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateStaffAsync(UpdateStaffDto dto)
        {
            var entity = await _context.Staff.FindAsync(dto.StaffId);

            if (entity == null)
                return false;

            entity.FullName = dto.FullName;
            entity.Position = dto.Position;
            entity.Department = dto.Department;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            entity.Address = dto.Address;
            entity.Salary = dto.Salary;

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteStaffAsync(int id)
        {
            var entity = await _context.Staff.FindAsync(id);

            if (entity == null)
                return false;

            _context.Staff.Remove(entity);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

    }
}

