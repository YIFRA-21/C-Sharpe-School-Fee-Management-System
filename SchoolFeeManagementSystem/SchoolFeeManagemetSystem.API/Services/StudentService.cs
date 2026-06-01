using SchoolFeeManagemetSystem.API.Interface;
using ScoolFeeManagementSystem.Data.Entities;
using static SchoolFeeManagemetSystem.API.DTOs.StudentDTOs;
using Microsoft.EntityFrameworkCore;
using ScoolFeeManagementSystem.Data.Context;

namespace SchoolFeeManagemetSystem.API.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDBContext _context;

        public StudentService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<StudentDTO>> GetAllAsync()
        {
            return await _context.Students
                .Select(s => new StudentDTO
                {
                    Id = s.Id,
                    AdmissionNo = s.AdmissionNo,
                    RollNo = s.RollNo,
                    FullName = s.FullName,
                    Class = s.Class,
                    Section = s.Section,
                    DateOfBirth = s.DateOfBirth,
                    Gender = s.Gender,
                    PhoneNumber = s.PhoneNo,
                    Email = s.Email,
                    Address = s.Address
                }).ToListAsync();
        }

        public async Task<StudentDTO> GetByIdAsync(int id)
        {
            var s = await _context.Students.FindAsync(id);
            if (s == null) return null;

            return new StudentDTO
            {
                Id = s.Id,
                AdmissionNo = s.AdmissionNo,
                RollNo = s.RollNo,
                FullName = s.FullName,
                Class = s.Class,
                Section = s.Section,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                PhoneNumber = s.PhoneNo,
                Email = s.Email,
                Address = s.Address
            };
        }

        public async Task<StudentDTO> CreateAsync(CreateStudentDTO dto)
        {
            var entity = new Student
            {
                AdmissionNo = dto.AdmissionNo,
                RollNo = dto.RollNo,
                FullName = dto.FullName,
                Class = dto.Class,
                Section = dto.Section,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                PhoneNo = dto.PhoneNumber,
                Email = dto.Email,
                Address = dto.Address
            };

            _context.Students.Add(entity);
            await _context.SaveChangesAsync();

            dto.GetType(); // just to avoid warning
            return await GetByIdAsync(entity.Id);
        }

        public async Task<bool> UpdateAsync(UpdateStudentDTO dto)
        {
            var entity = await _context.Students.FindAsync(dto.Id);
            if (entity == null) return false;

            entity.AdmissionNo = dto.AdmissionNo;
            entity.RollNo = dto.RollNo;
            entity.FullName = dto.FullName;
            entity.Class = dto.Class;
            entity.Section = dto.Section;
            entity.DateOfBirth = dto.DateOfBirth;
            entity.Gender = dto.Gender;
            entity.PhoneNo = dto.PhoneNumber;
            entity.Email = dto.Email;
            entity.Address = dto.Address;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Students.FindAsync(id);
            if (entity == null) return false;

            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
